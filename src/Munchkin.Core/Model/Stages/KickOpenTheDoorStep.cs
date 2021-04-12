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

        protected override async Task<Table> OnResolve(Table table)
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
