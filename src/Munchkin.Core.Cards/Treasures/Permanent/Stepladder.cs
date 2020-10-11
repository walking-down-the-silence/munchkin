using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Enums;
using Munchkin.Engine.Original.CardProperties;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Treasures
{
    public sealed class Stepladder : PermanentItemCard
    {
        public Stepladder() : base("Stepladder", 3, 0, EItemSize.Big, EWearingType.None, 400)
        {
            AddProperty(new HalflingOnlyRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}