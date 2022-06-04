using Munchkin.Core.Contracts;

namespace Munchkin.Core.Model.Attributes
{
    public record MaximumEquippedRacesAttribute(int Limit) :
        Attribute("Maximum Equipped Races");
}