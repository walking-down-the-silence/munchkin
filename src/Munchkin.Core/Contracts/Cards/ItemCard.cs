using Munchkin.Core.Extensions;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Attributes;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Contracts.Cards
{
    public abstract class ItemCard : TreasureCard
    {
        protected ItemCard(string title, int strength, int runAwayBonus, int goldPieces, EItemSize itemSize) : base(title)
        {
            AddProperty(new StrengthBonusAttribute(strength));
            AddProperty(new RunAwayBonusAttribute(runAwayBonus));
            AddProperty(new GoldPiecesAttribute(goldPieces));
            AddProperty(new ItemSizeAttribute(itemSize));
        }

        public int StrengthBonus => GetProperty<StrengthBonusAttribute>().Bonus;

        public int RunAwayBonus => GetProperty<RunAwayBonusAttribute>().Bonus;

        public int GoldPieces => GetProperty<GoldPiecesAttribute>().Gold;

        public EItemSize ItemSize => GetProperty<ItemSizeAttribute>().ItemSize;

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