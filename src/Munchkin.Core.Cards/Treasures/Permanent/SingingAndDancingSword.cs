using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Enums;
using Munchkin.Engine.Original.CardProperties;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Treasures
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