using Munchkin.Primitives.Abstractions;
using System.Collections.Immutable;

namespace Munchkin.Primitives.Immutable
{
    public static class ImmutableCardDeck
    {
        public static ImmutableCardDeck<TCard> Create<TCard>(IShuffleAlgorithm<TCard> shuffleAlgorithm, IEnumerable<TCard> cards)
        {
            return new ImmutableCardDeck<TCard>(shuffleAlgorithm, ImmutableList.CreateRange(cards));
        }

        public static ImmutableCardDeck<TCard> Create<TCard>(IShuffleAlgorithm<TCard> shuffleAlgorithm)
        {
            return new ImmutableCardDeck<TCard>(shuffleAlgorithm, ImmutableList<TCard>.Empty);
        }

        public static ImmutableCardDeck<TCard> Create<TCard>(IEnumerable<TCard> cards)
        {
            return new ImmutableCardDeck<TCard>(new DefaultShuffleAlgorithm<TCard>(), ImmutableList.CreateRange(cards));
        }
    }
}
