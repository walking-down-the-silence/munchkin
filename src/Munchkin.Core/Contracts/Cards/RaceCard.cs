namespace Munchkin.Core.Contracts.Cards
{
    public abstract class RaceCard : DoorsCard
    {
        protected RaceCard(string code, string title) : 
            base(code, title)
        {
        }
    }
}