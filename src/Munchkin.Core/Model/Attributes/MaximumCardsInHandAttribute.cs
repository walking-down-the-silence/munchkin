using Munchkin.Core.Contracts.Attributes;

namespace Munchkin.Core.Model.Attributes
{
    public record MaximumCardsInHandAttribute(int Limit) :
        Attribute("Maximum Cards In Hand");
}
