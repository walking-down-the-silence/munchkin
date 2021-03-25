using Munchkin.Core.Model;

namespace Munchkin.Core.Contracts.Cards
{
    public abstract class ClassCard : DoorsCard
    {
        protected ClassCard(string title) : base(title)
        {
        }

        public virtual void Equip(Table table, Player player)
        {
            player.PutInPlayAsEquipped(this);
        }
    }
}