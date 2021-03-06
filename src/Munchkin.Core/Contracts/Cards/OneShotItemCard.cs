using Munchkin.Core.Model;
using Munchkin.Core.Model.Attributes;
using Munchkin.Core.Model.Enums;
using System.Threading.Tasks;

namespace Munchkin.Core.Contracts.Cards
{
    public abstract class OneShotItemCard : ItemCard
    {
        protected OneShotItemCard(string title, int strength, int runAwayBonus, int price)
            : base(title, strength, runAwayBonus, price, EItemSize.Small)
        {
        }

        public override Task Play(Table context)
        {
            // TODO: check if current stage actually is a combat
            BonusAttribute strength = BoundTo is null
                ? new PlayerStrengthBonusAttribute(StrengthBonus)
                : new MonsterStrengthBonusAttribute(StrengthBonus);
            context.Dungeon.AddProperty(strength);
            context.Dungeon.AddProperty(new RunAwayBonusAttribute(RunAwayBonus));

            return Task.CompletedTask;
        }
    }
}