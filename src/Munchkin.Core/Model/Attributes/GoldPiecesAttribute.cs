using Munchkin.Core.Contracts.Attributes;

namespace Munchkin.Core.Model.Attributes
{
    public record GoldPiecesAttribute(int Gold) :
        Attribute("Gold Pieces");
}