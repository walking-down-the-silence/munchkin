namespace Munchkin.Primitives.Abstractions
{
    public interface ICardDeck<TCard> : IEnumerable<TCard>
    {
        bool IsEmpty { get; }

        void Shuffle();

        TCard Take();

        TCard Peek(int index);

        IEnumerable<TCard> TakeRange(int count);

        TTarget TakeFirst<TTarget>() where TTarget : TCard;

        TTarget TakeLast<TTarget>() where TTarget : TCard;

        void Put(TCard card);

        void PutRange(IEnumerable<TCard> items);
    }
}
