namespace Munchkin.Api.ViewModels.Trading
{
    public record TradeVM(
        string TradeId,
        TradeSideVM LeftSide,
        TradeSideVM RIghtSide);
}
