using Munchkin.Core.Contracts.Events;
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
            Effects.Where(effect => effect.Satisfies(context)).ForEach(effect => effect.Apply(context));

            return Task.CompletedTask;
        }

        public virtual void Sell(Table state)
        {
            var cardSoldEvent = new PlayerCardSoldEvent(Owner.Nickname, Code, GoldPieces);
            state.ActionLog.Add(cardSoldEvent);
        }
    }
}