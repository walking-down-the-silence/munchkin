using Munchkin.Core.Contracts.Cards;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class LargeAngryChicken : MonsterCard
    {
        public LargeAngryChicken() :
            base(MunchkinDeluxeCards.Doors.LargeAngryChicken, "Large Angry Chicken", 2, 1, 1, 0, false)
        {
        }

        public override Task Play(Table state)
        {
            // TODO: Add logic "gain an extra level if you defeat it with fire or flame"
            throw new NotImplementedException();
        }

        public override Table BadStuff(Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            player.LevelDown();
            return table;
        }
    }
}