using Munchkin.Core.Contracts.Attributes;

namespace Munchkin.Core.Model.Attributes
{
    public class GoldPiecesAttribute : Attribute
    {
        public GoldPiecesAttribute(int gold)
        {
            Gold = gold;
        }

        public int Gold { get; }
    }
}