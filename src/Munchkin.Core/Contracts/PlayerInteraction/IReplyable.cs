namespace Munchkin.Core.Contracts
{
    /// <summary>
    /// Contract tha allows to reply to the request
    /// </summary>
    public interface IReplyable<in TResult>
    {
        /// <summary>
        /// Replies to the request with resulting data
        /// </summary>
        void Reply(TResult result);

        void Cancel();
    }
}