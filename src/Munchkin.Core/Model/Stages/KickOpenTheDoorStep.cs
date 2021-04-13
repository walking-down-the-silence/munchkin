using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.Stages;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class KickOpenTheDoorStep : StepBase<Table>
    {
        public KickOpenTheDoorStep(Player currentPlayer) : base(StepNames.KickOpenTheDoor)
        {
            CurrentPlayer = currentPlayer ?? throw new System.ArgumentNullException(nameof(currentPlayer));
        }

        public Player CurrentPlayer { get; }

        public DoorsCard Card { get; private set; }

        protected override Task<Table> OnResolve(Table table)
        {
            var door = table.DoorsCardDeck.Take();

            table = door is not MonsterCard && door is not CurseCard
                ? TakeInHand(table, door)
                : PutInPlay(table, door);

            return Task.FromResult(table);
        }

        private Table TakeInHand(Table table, DoorsCard doorsCard)
        {
            // NOTE: if card is taking in hand then remove from played, so it is not discarded later
            table.Dungeon.RemovePlayedCard(doorsCard);
            table.Players.Current.TakeInHand(doorsCard);
            return table;
        }

        private Table PutInPlay(Table table, DoorsCard doorsCard)
        {
            // NOTE: if card not taken in hand, then it should be put in play
            table.Dungeon.AddPlayedCard(doorsCard);
            Card = doorsCard;
            return table;
        }
    }
}
