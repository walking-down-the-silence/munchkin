using Munchkin.Core.Contracts;

namespace Munchkin.Core.Model.Attributes
{
    public abstract record BonusAttribute(int Bonus) :
        Attribute("Bonus");
}
