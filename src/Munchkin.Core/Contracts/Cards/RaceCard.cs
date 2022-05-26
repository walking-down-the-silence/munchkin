using Munchkin.Core.Extensions;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Attributes;
using Munchkin.Core.Model.Exceptions;
using System;
using System.Linq;

namespace Munchkin.Core.Contracts.Cards
{
    public abstract class RaceCard : DoorsCard, IEquippable
    {
        protected RaceCard(string code, string title) :
            base(code, title)
        {
        }

        public void Equip(Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            var classesEquipped = player.Equipped.OfType<RaceCard>().Count();
            var equippedWithCheat = BoundTo != null && BoundTo.HasAttribute<CheatAttribute>();

            if (!equippedWithCheat && classesEquipped >= player.GetMaximumRacesEquipped())
                throw new CardCannotBeEquippedException("Player already has maximum races equipped.");

            player.Equip(this);
        }
    }
}