using Munchkin.Core.Model;

namespace Munchkin.Core.Contracts.Cards
{
    public abstract class RaceCard : DoorsCard, IEquippable
    {
        protected RaceCard(string code, string title) : 
            base(code, title)
        {
        }

        public void Equip(Table table, Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}