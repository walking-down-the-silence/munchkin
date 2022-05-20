using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Attributes;

namespace Munchkin.Core.Model.Attributes
{
    public record WearingTypeAttribute(EWearingType WearingType) :
        Attribute("Wearing Type");
}