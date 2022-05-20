using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Restrictions;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class CloakOfObscurity : WearingCard
    {
        public CloakOfObscurity() : 
            base(MunchkinDeluxeCards.Treasures.CloakOfObscurity, "Cloak Of Obscurity", 4, 0, EItemSize.Small, EWearingType.None, 600)
        {
            AddRestriction(new UsableByThiefOnlyRestriction());
        }
    }
}