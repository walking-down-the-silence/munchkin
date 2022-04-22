using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Attributes;

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