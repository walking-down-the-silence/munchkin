using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Attributes;

namespace Munchkin.Core.Model.Attributes
{
    public record ItemSizeAttribute(EItemSize ItemSize) :
        Attribute("Item Size");
}