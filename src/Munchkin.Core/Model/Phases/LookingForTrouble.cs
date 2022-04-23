using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Attributes;
using Munchkin.Core.Contracts.Cards;
using System.Collections.Immutable;
using System.Linq;

namespace Munchkin.Core.Model.Phases
{
    public record LookingForTrouble(
        Table Table,
        Player CurrentPlayer,
        ImmutableList<MonsterCard> Monsters
    ) : StateBase<LookingForTrouble>(Table, CurrentPlayer, ImmutableList<Attribute>.Empty)
    {
        public static IState From(Table table, Player currentPlayer)
        {
            var monsters = ImmutableList.CreateRange(currentPlayer.YourHand.OfType<MonsterCard>());
            return new LookingForTrouble(table, currentPlayer, monsters);
        }
    }

    public static class LookingForTroubleExtensions
    {
        public static IState SelectMonster(this LookingForTrouble state, MonsterCard monster)
        {
            state.CurrentPlayer.Discard(monster);
            return CombatRoom.From(state.Table, state.CurrentPlayer, monster);
        }
    }
}