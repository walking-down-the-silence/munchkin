using Munchkin.Core.Contracts;

namespace Munchkin.Core.Model.Attributes
{
    public record ItemSizeAttribute(EItemSize ItemSize) :
        Attribute("Item Size");
}