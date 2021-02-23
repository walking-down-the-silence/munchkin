using Munchkin.Core.Contracts.Attributes;

namespace Munchkin.Core.Model.Attributes
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
