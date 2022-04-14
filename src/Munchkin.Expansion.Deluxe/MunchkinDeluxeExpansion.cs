using Munchkin.Core.Contracts;

namespace Munchkin.Expansion.Deluxe
{
    public sealed class MunchkinDeluxeExpansion : IExpansion
    {
        public string Code => "munchkin.original.deluxe";

        public string Title => "Munchkin Deluxe";

        public IDoorDeckFactory DoorDeck => new MunchkinDeluxeDoorsFactory();

        public ITreasureDeckFactory TreasureDeck => new MunchkinDeluxeTreasuresFactory();
    }
}
