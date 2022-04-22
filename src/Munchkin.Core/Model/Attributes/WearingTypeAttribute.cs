using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Attributes;

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