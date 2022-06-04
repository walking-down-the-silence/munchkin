using Munchkin.Core.Contracts;

namespace Munchkin.Core.Model.Attributes
{
    public record WearingTypeAttribute(EWearingType WearingType) :
        Attribute("Wearing Type");
}