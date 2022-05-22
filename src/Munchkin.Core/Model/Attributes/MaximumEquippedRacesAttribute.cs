using Munchkin.Core.Contracts.Attributes;

namespace Munchkin.Core.Model.Attributes
{
    public record MaximumEquippedRacesAttribute(int Limit) :
        Attribute("Maximum Equipped Races");
}