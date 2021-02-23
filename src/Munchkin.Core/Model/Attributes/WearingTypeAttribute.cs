using Munchkin.Core.Contracts.Attributes;
using Munchkin.Core.Model.Enums;

namespace Munchkin.Core.Model.Attributes
{
    public class WearingTypeAttribute : Attribute
    {
        public WearingTypeAttribute(EWearingType wearingType)
        {
            WearingType = wearingType;
        }

        public EWearingType WearingType { get; }
    }
}