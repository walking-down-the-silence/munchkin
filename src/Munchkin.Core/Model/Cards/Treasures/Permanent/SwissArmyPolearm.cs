using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Attributes;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
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