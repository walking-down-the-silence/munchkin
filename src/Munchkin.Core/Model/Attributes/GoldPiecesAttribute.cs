using Munchkin.Core.Contracts;

namespace Munchkin.Core.Model.Attributes
{
    public record GoldPiecesAttribute(int Gold) :
        Attribute("Gold Pieces");
}