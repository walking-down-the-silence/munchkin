using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Restrictions;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Treasures.Permanent
{
    public sealed class CloakOfObscurity : PermanentItemCard
    {
        public CloakOfObscurity() : 
            base(MunchkinDeluxeCards.Treasures.CloakOfObscurity, "Cloak Of Obscurity", 4, 0, EItemSize.Small, EWearingType.None, 600)
        {
            AddRestriction(new UsableByThiefOnlyRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}