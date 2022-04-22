using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Attributes;
using Munchkin.Core.Contracts.Cards;
using System.Collections.Immutable;
using System.Linq;

namespace Munchkin.Core.Model.Phases
{
    public record LookForTrouble(
        Table Table,
        Player CurrentPlayer,
        ImmutableList<MonsterCard> Monsters
    )
    : StateBase<LookForTrouble>(Table, ImmutableList<Attribute>.Empty);

    public static class LookForTroubleExtensions
    {
        public static IState From(Table table, Player currentPlayer)
        {
            var monsters = ImmutableList.CreateRange(currentPlayer.YourHand.OfType<MonsterCard>());
            return new LookForTrouble(table, currentPlayer, monsters);
        }

        public static IState SelectMonster(this LookForTrouble state, MonsterCard monster)
        {
            state.CurrentPlayer.Discard(monster);
            return CombatRoomExtensions.From(state.Table, state.CurrentPlayer, monster);
        }
    }
}