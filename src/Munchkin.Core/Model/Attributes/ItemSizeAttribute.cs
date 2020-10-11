using Munchkin.Core.Model.Enums;

namespace Munchkin.Core.Model.Properties
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