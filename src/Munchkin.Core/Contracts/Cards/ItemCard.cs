using Munchkin.Core.Extensions;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Attributes;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Contracts.Cards
{
    public abstract class ItemCard : TreasureCard
    {
        protected ItemCard(string code, string title, int strength, int runAwayBonus, int goldPieces, EItemSize itemSize) : 
            base(code, title)
        {
            AddAttribute(new StrengthBonusAttribute(strength));
            AddAttribute(new RunAwayBonusAttribute(runAwayBonus));
            AddAttribute(new GoldPiecesAttribute(goldPieces));
            AddAttribute(new ItemSizeAttribute(itemSize));
        }

        public int StrengthBonus => GetAttribute<StrengthBonusAttribute>().Bonus;

        public int RunAwayBonus => GetAttribute<RunAwayBonusAttribute>().Bonus;

        public int GoldPieces => GetAttribute<GoldPiecesAttribute>().Gold;

        public EItemSize ItemSize => GetAttribute<ItemSizeAttribute>().ItemSize;

        public override Task Play(Table context)
        {
            // TODO: check if current stage actually is a combat
            //context.Dungeon.AddAtribute(new PlayerStrengthBonusAttribute(StrengthBonus));
            //context.Dungeon.AddAtribute(new RunAwayBonusAttribute(RunAwayBonus));

            // apply dynamic effects if conditions are met
            Effects.Where(effect => effect.Satisfies(context)).ForEach(effect => effect.Apply(context));

            return Task.CompletedTask;
        }

        public virtual void Sell(Table state)
        {
            //state.Dungeon.AddAtribute(new GoldPiecesAttribute(GoldPieces));
        }

        public virtual void Equip(Table state, Player player)
        {
            player.Equip(this);
        }
    }
}