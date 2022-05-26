using Munchkin.Core.Model;

namespace Munchkin.Core.Contracts.Cards
{
    public abstract class CurseCard : DoorsCard, ITakeBadStuff
    {
        protected CurseCard(string code, string title) : 
            base(code, title)
        {
        }

        public bool OneShot { get; protected set; }

        public abstract Table BadStuff(Table table, Player player);
    }
}