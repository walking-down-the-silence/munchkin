namespace Munchkin.Core.Model.Cards
{
    public abstract class CurseCard : DoorsCard
    {
        protected CurseCard(string title) : base(title)
        {
        }

        public bool OneShot { get; protected set; }
    }
}