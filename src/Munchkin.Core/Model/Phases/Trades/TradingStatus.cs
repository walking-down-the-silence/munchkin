namespace Munchkin.Core.Model.Phases.Trades
{
    /// <summary>
    /// Defines the states of the trading process bewteen players.
    /// </summary>
    public enum TradingStatus
    {
        /// <summary>
        /// The trade is active and ongoing.
        /// </summary>
        Ongoing,

        /// <summary>
        /// The trade was completed successfully by both parties.
        /// </summary>
        Successfull,

        /// <summary>
        /// The trade was rejected by one or both parties or timed out.
        /// </summary>
        Rejected
    }
}
