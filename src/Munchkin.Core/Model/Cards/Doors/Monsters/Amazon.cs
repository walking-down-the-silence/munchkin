using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Cards.Doors.Curses;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class Amazon : MonsterCard
    {
        public Amazon() :
            base(MunchkinDeluxeCards.Doors.Amazon, "Amazon", 8, 1, 2, 0, false)
        {
        }

        public override Task Play(Table table)
        {
            bool isFemale = table.Players.Current.Gender == EGender.Female
                            && !table.Players.Current.Equipped.OfType<ChangeSex>().Any()
                            || table.Players.Current.Gender == EGender.Male
                            && table.Players.Current.Equipped.OfType<ChangeSex>().Any();
            if (isFemale)
            {
                // TODO: implement +1 Treasure reward when isFemale
                //gameContext.Dungeon.RewardTreasures += 1;
            }
            else
            {
                base.Play(table);
            }

            BoundCards.ForEach(card => card.Play(table));

            return Task.CompletedTask;
        }

        public override Table BadStuff(Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            bool hasClasses = player.Equipped.OfType<ClassCard>().Any();

            if (hasClasses)
            {
                var classes = player.Equipped.OfType<ClassCard>();

                foreach (var classCard in classes)
                {
                    player.Discard(classCard);
                    table.DiscardedDoorsCards.Put(classCard);
                }
            }
            else
            {
                player.LevelDown(3);
            }

            return table;
        }
    }
}