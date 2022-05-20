namespace Munchkin.Core.Contracts.Cards
{
    public abstract class SpecialCard : DoorsCard
    {
        protected SpecialCard(string code, string title) : 
            base(code, title)
        {
        }
    }
}