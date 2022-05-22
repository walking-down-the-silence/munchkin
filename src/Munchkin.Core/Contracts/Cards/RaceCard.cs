using Munchkin.Core.Contracts.Exceptions;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model;
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

            if (classesEquipped >= player.GetMaximumRacesEquipped())
                throw new CardCannotBeEquippedException("Player already has maximum races equipped.");

            player.Equip(this);
        }
    }
}