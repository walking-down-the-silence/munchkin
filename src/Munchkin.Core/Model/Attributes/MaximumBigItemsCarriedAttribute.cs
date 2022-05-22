using Munchkin.Core.Contracts.Attributes;

namespace Munchkin.Core.Model.Attributes
{
    public record MaximumBigItemsCarriedAttribute(int Limit) :
        Attribute("Maximum Big Items Carried");
}