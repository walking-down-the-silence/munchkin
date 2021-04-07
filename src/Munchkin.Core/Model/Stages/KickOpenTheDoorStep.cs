using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class KickOpenTheDoorStep : IStep<Table>
    {
        public KickOpenTheDoorStep(Player currentPlayer)
        {
            CurrentPlayer = currentPlayer ?? throw new System.ArgumentNullException(nameof(currentPlayer));
        }

        public Player CurrentPlayer { get; }

        public DoorsCard Card { get; private set; }

        public async Task<Table> Resolve(Table table)
        {
            var door = table.DoorsCardDeck.Take();
            table.Dungeon.AddPlayedCard(door);
            Card = door;

            var stage = door switch
            {
                CurseCard curseCard     => new CursedRoomStage(curseCard),
                MonsterCard monsterCard => new CombatRoomStep(table.Players.Current, monsterCard),
                _                       => TakeInHand(table, door)
            };

            return await stage.Resolve(table);
        }

        private IStep<Table> TakeInHand(Table table, DoorsCard doorsCard)
        {
            // NOTE: if card is taking in hand then remove from played, so it is not discarded later
            table.Dungeon.RemovePlayedCard(doorsCard);
            table.Players.Current.TakeInHand(doorsCard);
            return new EmptyRoomStep();
        }
    }
}
