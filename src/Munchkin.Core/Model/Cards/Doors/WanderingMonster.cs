using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Attributes;

namespace Munchkin.Core.Model.Cards.Doors
{
    public sealed class WanderingMonster : SpecialCard
    {
        public WanderingMonster() :
            base(MunchkinDeluxeCards.Doors.WanderingMonster1, "Wandering Monster")
        {
            AddAttribute(new WanderingMonsterAttribute());
        }
    }
}