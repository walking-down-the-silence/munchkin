namespace Munchkin.Core.Contracts
{
    public interface IExpansion
    {
        string Code { get; }

        string Title { get; }

        IDoorDeckFactory DoorDeck { get; }

        ITreasureDeckFactory TreasureDeck { get; }
    }
}