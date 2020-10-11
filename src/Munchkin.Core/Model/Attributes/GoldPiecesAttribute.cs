namespace Munchkin.Core.Model.Properties
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