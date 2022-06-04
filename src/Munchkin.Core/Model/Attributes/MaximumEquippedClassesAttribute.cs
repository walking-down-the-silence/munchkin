using Munchkin.Core.Contracts;

namespace Munchkin.Core.Model.Attributes
{
    public record MaximumEquippedClassesAttribute(int Limit) :
        Attribute("Maximum Equipped Classes");
}