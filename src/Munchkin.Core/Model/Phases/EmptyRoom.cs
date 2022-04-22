using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Attributes;
using Munchkin.Core.Contracts.Cards;
using System.Collections.Immutable;

namespace Munchkin.Core.Model.Phases
{
    public record EmptyRoom(
        Table Table,
        Player CurrentPlayer
    )
    : StateBase<EmptyRoom>(Table, ImmutableList<Attribute>.Empty);

    public static class EmptyRoomExtensions
    {
        public static IState From(Table table, Player currentPlayer)
        {
            return new EmptyRoom(table, currentPlayer);
        }

        public static IState LootTheRoom(this EmptyRoom state)
        {
            // NOTE: 'Loot the Room' by drawing another cards from the Doors Deck
            var doors = state.Table.DoorsCardDeck.Take();
            state.Table.Players.Current.TakeInHand(doors);
            return CharityExtensions.From(state.Table, state.Table.Players.Current);
        }

        public static IState LookForTrouble(this EmptyRoom state, MonsterCard monster)
        {
            state.Table.Players.Current.Discard(monster);
            return LookForTroubleExtensions.From(state.Table, state.CurrentPlayer);
        }
    }
}