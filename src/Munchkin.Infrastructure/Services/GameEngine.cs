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
                        .From<SetupAvatarStep>(StepNames.SetupAvatar)
                        .To<KickOpenTheDoorStep>(s => new KickOpenTheDoorStep(table.Players.Current)))
                    .Transition(x => x
                        .From<KickOpenTheDoorStep>(StepNames.KickOpenTheDoor)
                        .To<CombatRoomStep>(CreateCombatStep, CanTransitionToCombat)
                        .To<CurseStep>(CreateCurseRoomStep, CanTransitionToCurse)
                        .To<EmptyRoomStep>(CreateEmptyRoom, CardIsNotMonsterAndNoteCurse))
                    .Transition(x => x
                        .From<CurseStep>(StepNames.Curse)
                        .To<EmptyRoomStep>(CreateEmptyRoom, CanTransitionToEmptyRoom))
                    .Transition(x => x
                        .From<CombatRoomStep>(StepNames.Combat)
                        .To<RunAwayStep>(CreateRunAway, CanTransitionToRunAway)
                        .To<CharityStep>(CreateCharity, CanTransitionToCharity))
                    .Transition(x => x
                        .From<EmptyRoomStep>(StepNames.EmptyRoom)
                        .To<LookForTroubleStep>(CreateLookForTrouble, CanTransitionToLookForTrouble)
                        .To<LootTheRoomStep>(CreateLootTheRoom, CanTransitionToLootTheRoom))
                    .Transition(x => x
                        .From<LookForTroubleStep>(StepNames.LookForTrouble)
                        .To<CombatRoomStep>(CreateCombatStep, CanTransitionToCombat))
                    .Transition(x => x
                        .From<LootTheRoomStep>(StepNames.LootTheRoom)
                        .To<CharityStep>(CreateCharity, CanTransitionToCharity))
                    .Transition(x => x
                        .From<RunAwayStep>(StepNames.RunAway)
                        .To<DeathStep>(CreateDeathStep, CanTransitionToDeathStep))
                    .Transition(x => x
                        .From<CharityStep>(StepNames.Charity)
                        .To<EndStep>(CreateEndStep, CanTransitionToEnd))
                    .Transition(x => x
                        .From<DeathStep>(StepNames.Death)
                        .To<EndStep>(CreateEndStep, CanTransitionToEnd))
                    .Build();

                // NOTE: wait for each player to actually end the turn by executing action
                var initialStep = new SetupAvatarStep();
                table = await decisionGraph.Resolve(table, initialStep);

                history = history.Push(table);

                // NOTE: clear/reset the state befor moving to next turn
                table.Dungeon.Reset();
                table.Players.Next();
            }

            return table;
        }

        private bool CanTransitionToCombat(KickOpenTheDoorStep source) => source.Card is MonsterCard;

        private CombatRoomStep CreateCombatStep(KickOpenTheDoorStep step) => new(step.CurrentPlayer, step.Card as MonsterCard);

        private CombatRoomStep CreateCombatStep(LookForTroubleStep step) => new(null, null);

        private bool CanTransitionToCombat(LookForTroubleStep step) => true;

        private bool CanTransitionToCurse(KickOpenTheDoorStep source) => source.Card is CurseCard;

        private CurseStep CreateCurseRoomStep(KickOpenTheDoorStep step) => new(step.Card as CurseCard);

        private bool CardIsNotMonsterAndNoteCurse(KickOpenTheDoorStep step) => step.Card is not MonsterCard && step.Card is not CurseCard;

        private EmptyRoomStep CreateEmptyRoom(KickOpenTheDoorStep step) => new();

        private EmptyRoomStep CreateEmptyRoom(CurseStep step) => new();

        private bool CanTransitionToEmptyRoom(CurseStep step) => true;

        private RunAwayStep CreateRunAway(CombatRoomStep step) => new(step.FightingPlayer, step.HelpingPlayer, step.Monsters);

        private bool CanTransitionToRunAway(CombatRoomStep step) => true;

        private CharityStep CreateCharity(CombatRoomStep step) => new();

        private bool CanTransitionToCharity(CombatRoomStep step) => true;

        private CharityStep CreateCharity(LootTheRoomStep step) => new();

        private bool CanTransitionToCharity(LootTheRoomStep step) => true;

        private LookForTroubleStep CreateLookForTrouble(EmptyRoomStep step) => new();

        private bool CanTransitionToLookForTrouble(EmptyRoomStep step) => true;

        private LootTheRoomStep CreateLootTheRoom(EmptyRoomStep step) => new();

        private bool CanTransitionToLootTheRoom(EmptyRoomStep step) => true;

        private DeathStep CreateDeathStep(RunAwayStep step) => new();

        private bool CanTransitionToDeathStep(RunAwayStep step) => true;

        private EndStep CreateEndStep(CharityStep step) => new();

        private bool CanTransitionToEnd(CharityStep step) => true;

        private EndStep CreateEndStep(DeathStep step) => new();

        private bool CanTransitionToEnd(DeathStep step) => true;
    }
}
