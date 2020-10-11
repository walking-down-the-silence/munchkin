using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Enums;
using Munchkin.Engine.Original.CardProperties;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Treasures
{
    public sealed class CloakOfObscurity : PermanentItemCard
    {
        public CloakOfObscurity() : base("Cloak Of Obscurity", 4, 0, EItemSize.Small, EWearingType.None, 600)
        {
            AddProperty(new TheifOnlyRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}