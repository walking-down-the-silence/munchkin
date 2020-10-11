using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Enums;
using Munchkin.Engine.Original.CardProperties;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Treasures
{
    public sealed class SwissArmyPolearm : PermanentItemCard
    {
        public SwissArmyPolearm() : base("Swiss Army Polearm", 4, 0, EItemSize.Big, EWearingType.TwoHanded, 600)
        {
            AddProperty(new HumanOnlyRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}