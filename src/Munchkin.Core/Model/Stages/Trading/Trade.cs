namespace Munchkin.Core.Stages
{
    /// <summary>
    /// Defines the state of trading cards between players.
    /// </summary>
    /// <param name="LeftSide">The left side of the trade.</param>
    /// <param name="RightSide">The right side of the trade.</param>
    public record Trade(
        TradingSide LeftSide,
        TradingSide RightSide,
        TradingStatus Status,
        PlayerTradeChoice LeftSideTradeDecision,
        PlayerTradeChoice RightSideTradeDecision);
}
