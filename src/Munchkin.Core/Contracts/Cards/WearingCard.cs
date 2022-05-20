using Munchkin.Core.Contracts.Exceptions;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Attributes;
using System.Linq;

namespace Munchkin.Core.Contracts.Cards
{
    public abstract class WearingCard : ItemCard, IEquippable
    {
        protected WearingCard(string code, string title, int strength, int runAwayBonus, EItemSize itemSize, EWearingType wearingType, int goldPieces)
            : base(code, title, strength, runAwayBonus, goldPieces, itemSize)
        {
            AddAttribute(new WearingTypeAttribute(wearingType));
        }

        public EWearingType WearingType => GetAttribute<WearingTypeAttribute>().WearingType;

        public virtual void Equip(Table state, Player player)
        {
            if (player is not null)
            {
                if (player.Equipped.OfType<WearingCard>().Any(x => x.WearingType == WearingType))
                    throw new CardCannotBeEquippedException($"Player already wears an item of type {WearingType}.");

                var unsatisfied = Restrictions.Where(x => !x.Satisfies(state));

                // TODO: Think of having the 'reason' for the rule to pass to the exception
                if (unsatisfied.Any())
                    throw new CardCannotBeEquippedException("At least one restriction was not satisfied.");

                player.Equip(this);
            }
        }
    }
}