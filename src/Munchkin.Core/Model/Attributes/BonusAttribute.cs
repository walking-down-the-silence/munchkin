namespace Munchkin.Core.Model.Properties
{
    public abstract class BonusAttribute : Attribute
    {
        protected BonusAttribute(int bonus)
        {
            Bonus = bonus;
        }

        public int Bonus { get; }
    }
}
