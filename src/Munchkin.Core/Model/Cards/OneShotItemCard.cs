using Munchkin.Core.Model.Enums;
using Munchkin.Core.Model.Properties;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards
{
    public abstract class OneShotItemCard : ItemCard
    {
        protected OneShotItemCard(string title, int strength, int runAwayBonus, int price)
            : base(title, strength, runAwayBonus, price, EItemSize.Small)
        {
        }

        public override Task Play(Table context)
        {
            BonusAttribute strength = BoundTo is null
                ? (BonusAttribute) new PlayerStrengthBonusAttribute(StrengthBonus)
                : (BonusAttribute) new MonsterStrengthBonusAttribute(StrengthBonus);
            context.Dungeon.Combat.AddProperty(strength);
            context.Dungeon.Combat.AddProperty(new RunAwayBonusAttribute(RunAwayBonus));

            return Task.CompletedTask;
        }
    }
}