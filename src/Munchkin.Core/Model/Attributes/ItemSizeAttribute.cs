using Munchkin.Core.Contracts.Attributes;
using Munchkin.Core.Model.Enums;

namespace Munchkin.Core.Model.Attributes
{
    public class ItemSizeAttribute : Attribute
    {
        public ItemSizeAttribute(EItemSize itemSize)
        {
            ItemSize = itemSize;
        }

        public EItemSize ItemSize { get; }
    }
}