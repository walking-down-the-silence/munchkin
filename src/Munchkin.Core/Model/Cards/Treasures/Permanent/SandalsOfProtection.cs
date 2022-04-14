using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Enums;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Treasures
{
    public sealed class SandalsOfProtection : PermanentItemCard
    {
        public SandalsOfProtection() : base("Sandals Of Protection", 0, 0, EItemSize.Small, EWearingType.Footgear, 700)
        {
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}