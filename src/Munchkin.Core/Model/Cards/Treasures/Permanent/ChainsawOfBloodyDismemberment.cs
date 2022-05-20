using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class ChainsawOfBloodyDismemberment : PermanentItemCard
    {
        public ChainsawOfBloodyDismemberment() :
            base(MunchkinDeluxeCards.Treasures.ChainsawOfBloodyDismemberment, "Chainsaw Of Bloody Dismemberment", 3, 0, EItemSize.Big, EWearingType.TwoHanded, 600)
        {
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}