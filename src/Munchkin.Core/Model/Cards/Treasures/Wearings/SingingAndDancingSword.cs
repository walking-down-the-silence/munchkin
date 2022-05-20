using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Restrictions;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class SingingAndDancingSword : WearingCard
    {
        public SingingAndDancingSword() :
            base(MunchkinDeluxeCards.Treasures.SingingAndDancingSword, "Singing And Dancing Sword", 2, 0, EItemSize.Small, EWearingType.None, 400)
        {
            AddRestriction(new NotUsableByThiefRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}