using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Enums;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Treasures
{
    public sealed class ReallyImpressiveTitle : PermanentItemCard
    {
        public ReallyImpressiveTitle() : base("Really Impressive Title", 3, 0, EItemSize.Small, EWearingType.None, 0)
        {
        }

        public override Task Play(Table gameContext)
        {
            throw new System.NotImplementedException();
        }
    }
}