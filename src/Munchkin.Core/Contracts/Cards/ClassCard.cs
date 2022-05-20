using Munchkin.Core.Contracts.Exceptions;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Attributes;
using System.Linq;

namespace Munchkin.Core.Contracts.Cards
{
    public abstract class ClassCard : DoorsCard, IEquippable
    {
        protected ClassCard(string code, string title) :
            base(code, title)
        {
        }

        public virtual void Equip(Table table, Player player)
        {
            if (player is not null)
            {
                var twoClassesAllowed = player.Equipped.Any(x => x.HasAttribute<TwoClassesAllowed>());
                var classesEquipped = player.Equipped.OfType<ClassCard>().Count();

                // NOTE: Either no classes are equipped OR zero or one classes are equipped and player has Halfbreed card.
                if (twoClassesAllowed && classesEquipped >= 2 || classesEquipped > 0)
                    throw new CardCannotBeEquippedException("Player has maximum classes equipped.");

                player.Equip(this);
            }
        }
    }
}