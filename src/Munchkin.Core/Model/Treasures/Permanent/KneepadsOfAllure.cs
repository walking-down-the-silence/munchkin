using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Attributes;
using Munchkin.Core.Model.Enums;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Treasures
{
    public sealed class KneepadsOfAllure : PermanentItemCard
    {
        public KneepadsOfAllure() : base("Kneepads Of Allure", 0, 0, EItemSize.Small, EWearingType.None, 600)
        {
            AddProperty(new NotForClericRestriction());
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}