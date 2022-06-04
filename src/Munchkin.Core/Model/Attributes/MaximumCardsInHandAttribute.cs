using Munchkin.Core.Contracts;

namespace Munchkin.Core.Model.Attributes
{
    public record MaximumCardsInHandAttribute(int Limit) :
        Attribute("Maximum Cards In Hand");
}
