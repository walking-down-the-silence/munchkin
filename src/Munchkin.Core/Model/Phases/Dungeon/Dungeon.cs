using Munchkin.Core.Contracts.Cards;
using System;
using System.Collections.Immutable;

namespace Munchkin.Core.Model.Phases
{
    /// <summary>
    /// Defines the state of the dungeon that the player has entered.
    /// </summary>
    public record Dungeon(
        Table Table,
        Player CurrentPlayer,
        Combat Combat,
        Cursing Cursed,
        RunningAway RunningAway,
        Death Death,
        Charity Charity,
        ImmutableArray<Card> TemporaryPile)
    {
        public static Dungeon From(Table table, Player currentPlayer)
        {
            return new Dungeon(
                table,
                currentPlayer,
                Combat.From(table, currentPlayer),
                Cursing.From(table, currentPlayer),
                RunningAway.From(table, currentPlayer, null, -1),
                Death.From(table, currentPlayer),
                Charity.From(table, currentPlayer),
                ImmutableArray<Card>.Empty);
        }

        public static Dungeon Reduce(Dungeon state, IDungeonAction action)
        {
            return action switch
            {
                KickOpenTheDoorAction _             => KickOpenTheDoor(state),
                LootTheRoomAction _                 => LootTheRoom(state),
                LookForTroubleAction lookForTrouble => LookForTrouble(state, lookForTrouble.Monster),
                PlayCardAction playCard             => Play(null, playCard.Card),
                ICombatAction combat                => state with { Combat = Combat.Reduce(state.Combat, combat) },
                ICurseAction curse                  => state with { Cursed = Cursing.Reduce(state.Cursed, curse) },
                IRunningAwayAction runningAway      => state with { RunningAway = RunningAway.Reduce(state.RunningAway, runningAway) },
                IDeathAction death                  => state with { Death = Death.Reduce(state.Death, death) },
                ICharityAction charity              => state with { Charity = Charity.Reduce(state.Charity, charity) },
                _                                   => throw new ArgumentOutOfRangeException(nameof(action))
            };
        }

        #region Dungeon Actions

        public static Dungeon KickOpenTheDoor(Dungeon state)
        {
            // NOTE: 'Kick Open the Door' by drawing a card from the Doors Deck
            var doors = state.Table.DoorsCardDeck.Take();

            var nextState = doors switch
            {
                CurseCard curse => CreateCusedRoomActionResult(state, curse),
                MonsterCard monster => CreateCombatRoomActionResult(state, monster),
                _ => CreateEmptyRoomActionResult(state, doors)
            };

            return nextState;
        }

        public static Dungeon LootTheRoom(Dungeon state)
        {
            // NOTE: 'Loot the Room' by drawing another cards from the Doors Deck
            var doors = state.Table.DoorsCardDeck.Take();
            state.CurrentPlayer.TakeInHand(doors);
            var availableActions = ImmutableArray.CreateRange(TurnActions.Player.All);
            //return ActionResult.Create(state, availableActions);
            return state;
        }

        public static Dungeon LookForTrouble(Dungeon state, MonsterCard monster)
        {
            state.CurrentPlayer.Discard(monster);
            var availableActions = ImmutableArray.CreateRange(TurnActions.Combat.All);
            state = state with { TemporaryPile = state.TemporaryPile.Add(monster) };
            //return ActionResult.Create(state, availableActions);
            return state;
        }

        public static Dungeon Curse(Dungeon state, CurseCard curse, Player player)
        {
            var availableActions = ImmutableArray.CreateRange(TurnActions.Curse.All);
            state = state with { TemporaryPile = state.TemporaryPile.Add(curse) };
            //return ActionResult.Create(state, availableActions);
            return state;
        }

        public static Dungeon Play(Dungeon state, Card card)
        {
            card.Play(state.Table);
            var availableActions = ImmutableArray.CreateRange(TurnActions.Player.All);
            state = state with { TemporaryPile = state.TemporaryPile.Add(card) };
            //return ActionResult.Create(state, availableActions);
            return state;
        }

        #endregion

        #region Private Methods

        private static Dungeon CreateCusedRoomActionResult(Dungeon state, CurseCard curse)
        {
            var availableActions = ImmutableArray.CreateRange(TurnActions.Curse.All);
            state = state with { TemporaryPile = state.TemporaryPile.Add(curse) };
            //return ActionResult.Create(state, availableActions);
            return state;
        }

        private static Dungeon CreateCombatRoomActionResult(Dungeon state, MonsterCard monster)
        {
            var availableActions = ImmutableArray.CreateRange(TurnActions.Combat.All);
            state = state with { TemporaryPile = state.TemporaryPile.Add(monster) };
            //return ActionResult.Create(state, availableActions);
            return state;
        }

        private static Dungeon CreateEmptyRoomActionResult(Dungeon state, DoorsCard card)
        {
            // NOTE: If acquired some other way, such as by Looting The Room, Curse cards
            // go into your hand and may be played on any player at any time.
            state.CurrentPlayer.TakeInHand(card);
            var availableActions = ImmutableArray.CreateRange(new string[]
            {
                TurnActions.Dungeon.LootTheRoom,
                TurnActions.Dungeon.LookForTrouble
            });
            state = state with { TemporaryPile = state.TemporaryPile.Add(card) };
            //return ActionResult.Create(state, availableActions);
            return state;
        }

        #endregion
    }
}