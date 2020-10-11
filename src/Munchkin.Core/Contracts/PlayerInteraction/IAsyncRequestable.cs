using Munchkin.Core.Model;
using Munchkin.Core.PlayerInteraction;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Contracts
{
    public interface IAsyncRequestable<TResult> : IObservable<AsyncRequestActionEvent<TResult>>
    {
        /// <summary>
        /// Creates and event for the request and notifies subscribers about it
        /// </summary>
        Task<TResult> RequestAsync(Player target, Type requestedDataType);
    }
}