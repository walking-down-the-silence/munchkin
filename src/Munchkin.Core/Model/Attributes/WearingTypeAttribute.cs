using Munchkin.Core.Model.Enums;

namespace Munchkin.Core.Model.Properties
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