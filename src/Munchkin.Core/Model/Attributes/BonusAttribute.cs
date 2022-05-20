using Munchkin.Core.Contracts.Attributes;

namespace Munchkin.Core.Model.Attributes
{
    public abstract record BonusAttribute(int Bonus) :
        Attribute("Bonus");
}
