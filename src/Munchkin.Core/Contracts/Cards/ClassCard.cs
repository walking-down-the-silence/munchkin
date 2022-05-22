using Munchkin.Core.Contracts.Exceptions;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model;
using System;
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
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            var classesEquipped = player.Equipped.OfType<ClassCard>().Count();

            if (classesEquipped >= player.GetMaximumClassesEquipped())
                throw new CardCannotBeEquippedException("Player already has maximum classes equipped.");

            player.Equip(this);
        }
    }
}