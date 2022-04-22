using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Attributes;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class SingingAndDancingSword : PermanentItemCard
    {
        public SingingAndDancingSword() : base("Singing And Dancing Sword", 2, 0, EItemSize.Small, EWearingType.None, 400)
        {
            AddProperty(new NotForThiefRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}