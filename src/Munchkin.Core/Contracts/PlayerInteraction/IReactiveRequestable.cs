using Munchkin.Core.Model;
using Munchkin.Core.PlayerInteraction;
using System;

namespace Munchkin.Core.Contracts
{
    /// <summary>
    /// Contract that allows to create a request from one player to another
    /// </summary>
    public interface IReactiveRequestable<TResult> : IObservable<ReactiveRequestActionEvent<TResult>>, IObservable<TResult>
    {
        /// <summary>
        /// Creates and event for the request and notifies subscribers about it
        /// </summary>
        void RequestAsync(Player target, Type requestedDataType);
    }
}