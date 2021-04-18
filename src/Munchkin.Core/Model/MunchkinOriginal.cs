using Munchkin.Core.Contracts;

namespace Munchkin.Core.Model
{
    public sealed class MunchkinOriginal : IExpansion
    {
        public string Code => "munchkin.original";

        public string Title => "Munchkin Deluxe";

        public IDoorDeckFactory DoorDeck => new MunchkinOriginalDoorsFactory();

        public ITreasureDeckFactory TreasureDeck => new MunchkinOriginalTreasuresFactory();
    }
}
