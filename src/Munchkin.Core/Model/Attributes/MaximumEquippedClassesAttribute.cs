using Munchkin.Core.Contracts.Attributes;

namespace Munchkin.Core.Model.Attributes
{
    public record MaximumEquippedClassesAttribute(int Limit) :
        Attribute("Maximum Equipped Classes");
}