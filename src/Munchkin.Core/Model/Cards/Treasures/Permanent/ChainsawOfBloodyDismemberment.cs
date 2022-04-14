using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Enums;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Treasures
{
    public sealed class ChainsawOfBloodyDismemberment : PermanentItemCard
    {
        public ChainsawOfBloodyDismemberment() : base("Chainsaw Of Bloody Dismemberment", 3, 0, EItemSize.Big, EWearingType.TwoHanded, 600)
        {
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}