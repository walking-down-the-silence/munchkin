namespace Munchkin.Core.Stages
{
    /// <summary>
    /// Defines the choice a player makes regarding the trade offer.
    /// </summary>
    public enum PlayerTradeChoice
    {
        /// <summary>
        /// The decision is not yet defined by the player.
        /// </summary>
        None,

        /// <summary>
        /// The player decision to confirm the trade.
        /// </summary>
        Confirm,

        /// <summary>
        /// The player decision to reject the trade.
        /// </summary>
        Reject
    }
}
