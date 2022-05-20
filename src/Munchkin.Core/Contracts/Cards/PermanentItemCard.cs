using Munchkin.Core.Model.Attributes;

namespace Munchkin.Core.Contracts.Cards
{
    public abstract class PermanentItemCard : ItemCard
    {
        protected PermanentItemCard(string code, string title, int strength, int runAwayBonus, EItemSize itemSize, EWearingType wearingType, int goldPieces)
            : base(code, title, strength, runAwayBonus, goldPieces, itemSize)
        {
            AddAttribute(new WearingTypeAttribute(wearingType));
        }

        public EWearingType WearingType => GetAttribute<WearingTypeAttribute>().WearingType;
    }
}