namespace Munchkin.Core.Model.Phases.Trades
{
    /// <summary>
    /// Defines the state of trading cards between players.
    /// </summary>
    /// <param name="LeftSide">The left side of the trade.</param>
    /// <param name="RightSide">The right side of the trade.</param>
    /// <param name="Status">The outcome of the trade based on both players decisions.</param>
    public record Trade(
        TradingSide LeftSide,
        TradingSide RightSide,
        TradingStatus Status);
}
