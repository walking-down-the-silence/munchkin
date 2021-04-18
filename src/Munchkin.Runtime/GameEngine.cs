using MediatR;
using Munchkin.Core;
using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.Stages;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Stages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Runtime
{
    public class GameEngine
    {
        private readonly IMediator _mediator;
        private readonly IReadOnlyCollection<IExpansion> _expansions;
        private readonly IReadOnlyCollection<Player> _players;
        private Table _table;

        public GameEngine(
            IMediator mediator,
            IReadOnlyCollection<IExpansion> expansions,
            IReadOnlyCollection<Player> players)
        {
            _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));
            _expansions = expansions ?? throw new System.ArgumentNullException(nameof(expansions));
            _players = players ?? throw new System.ArgumentNullException(nameof(players));
        }

        public Table Table => _table;

        public async Task<Table> RunAsync()
        {
            // NOTE: setup the table before the game starts
            var playersList = new CircularList<Player>(_players);

            _table = Table.Empty().WithRequestSink(_mediator);
            _table = _expansions
                .Aggregate(_table, (table, expansion) => table
                    .WithTreasureDeck(expansion.TreasureDeck.GetTreasureCards())
                    .WithDoorDeck(expansion.DoorDeck.GetDoorsCards()));

            // NOTE: shuffle the decks for randomness
            _table.DoorsCardDeck.Shuffle();
            _table.TreasureCardDeck.Shuffle();

            // NOTE: give all players initial cards
            _table.Players.ForEach(player => player.Revive(_table));

            while (!_table.IsGameWon)
            {
                var playerTurn = CreatePlayerTurn(_table);

                // NOTE: wait for each player to actually end the turn by executing action
                var initialStep = new ReviveAndSetupAvatarStep(_table.Players.Current);
                _table = await playerTurn.Resolve(_table, initialStep);

                // NOTE: clear/reset the state befor moving to next turn
                _table.Dungeon.Reset();
                _table.Players.Next();
            }

            return _table;
        }

        private DecisionGraph CreatePlayerTurn(Table table)
        {
            return DecisionGraph
                .Empty()
                .Transition(x => x
                    .From<ReviveAndSetupAvatarStep>(StepNames.ReviveAndSetupAvatar)
                    .To(s => new KickOpenTheDoorStep(table.Players.Current)))
                .Transition(x => x
                    .From<KickOpenTheDoorStep>(StepNames.KickOpenTheDoor)
                    .To(CreateCombatStep, CanTransitionToCombat)
                    .To(CreateCurseRoomStep, CanTransitionToCurse)
                    .To(CreateEmptyRoom, CardIsNotMonsterAndNoteCurse))
                .Transition(x => x
                    .From<CurseStep>(StepNames.Curse)
                    .To(CreateEmptyRoom, CanTransitionToEmptyRoom))
                .Transition(x => x
                    .From<CombatRoomStep>(StepNames.Combat)
                    .To(CreateRunAway, CanTransitionToRunAway)
                    .To(CreateCharity, CanTransitionToCharity))
                .Transition(x => x
                    .From<EmptyRoomStep>(StepNames.EmptyRoom)
                    .To(CreateLookForTrouble, CanTransitionToLookForTrouble)
                    .To(CreateLootTheRoom, CanTransitionToLootTheRoom))
                .Transition(x => x
                    .From<LookForTroubleStep>(StepNames.LookForTrouble)
                    .To(CreateCombatStep, CanTransitionToCombat))
                .Transition(x => x
                    .From<LootTheRoomStep>(StepNames.LootTheRoom)
                    .To(CreateCharity, CanTransitionToCharity))
                .Transition(x => x
                    .From<RunAwayStep>(StepNames.RunAway)
                    .To(CreateDeathStep, CanTransitionToDeathStep))
                .Transition(x => x
                    .From<CharityStep>(StepNames.Charity)
                    .To(CreateEndStep, CanTransitionToEnd))
                .Transition(x => x
                    .From<DeathStep>(StepNames.Death)
                    .To(CreateEndStep, CanTransitionToEnd))
                .Build();
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
