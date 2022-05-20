using Munchkin.Core.Model;

namespace Munchkin.Core.Contracts.Cards
{
    public abstract class ClassCard : DoorsCard
    {
        protected ClassCard(string code, string title) : 
            base(code, title)
        {
        }

        public virtual void Equip(Table table, Player player)
        {
            player.Equip(this);
        }
    }
}