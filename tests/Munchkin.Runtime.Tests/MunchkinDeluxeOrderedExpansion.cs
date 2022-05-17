using Munchkin.Core.Contracts;

namespace Munchkin.Runtime.Tests
{
    internal class MunchkinDeluxeOrderedExpansion : IExpansion
    {
        public string Code => "munchkin.original.deluxe-test";

        public string Title => "Munchkin Deluxe (Test)";

        public IDoorDeckFactory DoorDeck => new MunchkinDeluxeDoorsFactoryTest();

        public ITreasureDeckFactory TreasureDeck => new MunchkinDeluxeTreasuresFactoryTest();
    }
}
