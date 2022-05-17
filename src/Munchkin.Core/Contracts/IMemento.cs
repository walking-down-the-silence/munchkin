namespace Munchkin.Core.Contracts
{
    public interface IImmutableMemento<TState>
    {
        TState Apply(TState state);

        TState Revert();
    }
}
