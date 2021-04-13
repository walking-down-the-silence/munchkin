using MediatR;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.Stages;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Stages;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace Munchkin.Infrastructure.Services
{
    public class GameEngine
    {
        private readonly IMediator _mediator;

        public GameEngine(IMediator mediator)
        {
            _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));
        }

        public async Task<Table> NextTurn()
        {
            var stage = new SetupTableStep();
            var table = await stage.Resolve(new Table(_mediator));
            var history = ImmutableStack<Table>.Empty;

            while (!table.IsGameWon)
            {
                var decisionGraph = DecisionGraph
                    .Empty()
                    .Transition(x => x
                        .From<ResetActionsStep>(StepNames.ResetActions)
                        .To<SetupAvatarStep>(s => new SetupAvatarStep()))
                    .Transition(x => x
                        .From<SetupAvatarStep>(StepNames.SetupAvatar)
                        .To<KickOpenTheDoorStep>(s => new KickOpenTheDoorStep(table.Players.Current)))
                    .Transition(x => x
                        .From<KickOpenTheDoorStep>(StepNames.KickOpenTheDoor)
                        .To<CombatRoomStep>(
                            configCreation: s => new CombatRoomStep(s.CurrentPlayer, s.Card as MonsterCard),
                            configCondition: s => s.Card is MonsterCard)
                        .To<CurseStep>(
                            configCreation: s => new CurseStep(s.Card as CurseCard),
                            configCondition: s => s.Card is CurseCard)
                        .To<EmptyRoomStep>(
                            configCreation: s => new EmptyRoomStep(),
                            configCondition: s => s.Card is not MonsterCard && s.Card is not CurseCard))
                    .Transition(x => x
                        .From<CursedRoomStage>(StepNames.Curse)
                        .To<EmptyRoomStep>(s => new EmptyRoomStep(), s => true))
                    .Transition(x => x
                        .From<CombatRoomStep>(StepNames.Combat)
                        .To<RunAwayStep>(s => new RunAwayStep(s.FightingPlayer, s.HelpingPlayer, s.Monsters), s => true)
                        .To<CharityStep>(s => new CharityStep(), s => true))
                    .Transition(x => x
                        .From<EmptyRoomStep>(StepNames.EmptyRoom)
                        .To<LookForTroubleStep>(s => new LookForTroubleStep(), s => true)
                        .To<LootTheRoomStep>(s => new LootTheRoomStep(), s => true))
                    .Transition(x => x
                        .From<LookForTroubleStep>(StepNames.LookForTrouble)
                        .To<CombatRoomStep>(s => new CombatRoomStep(null, null), s => true))
                    .Transition(x => x
                        .From<LootTheRoomStep>(StepNames.LootTheRoom)
                        .To<CharityStep>(s => new CharityStep(), s => true))
                    .Transition(x => x
                        .From<RunAwayStep>(StepNames.RunAway)
                        .To<DeathStep>(s => new DeathStep(), s => true))
                    .Transition(x => x
                        .From<CharityStep>(StepNames.Charity)
                        .To<EndStep>(s => new EndStep(), s => true))
                    .Transition(x => x
                        .From<DeathStep>(StepNames.Death)
                        .To<EndStep>(s => new EndStep(), s => true))
                    .Build();

                // NOTE: wait for each player to actually end the turn by executing action
                var initialStep = new ResetActionsStep();
                table = await decisionGraph.Resolve(table, initialStep);

                history = history.Push(table);

                // NOTE: clear/reset the state befor moving to next turn
                table.Dungeon.Reset();
                table.Players.Next();
            }

            return table;
        }
    }
}
