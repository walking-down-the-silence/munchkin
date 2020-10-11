using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Core.PlayerInteraction
{
    public class AsyncRequestSink<TResult> : IAsyncRequestable<TResult>
    {
        private readonly List<AsyncRequestActionEvent<TResult>> _requests = new List<AsyncRequestActionEvent<TResult>>();
        private IObserver<AsyncRequestActionEvent<TResult>> _clientObserver;

        public Task<TResult> RequestAsync(Player target, Type requestedDataType)
        {
            // invoke the subscribed method on the client to handle the actual request
            var completionSource = new TaskCompletionSource<TResult>();
            var requestEvent = new AsyncRequestActionEvent<TResult>(_requests, completionSource);
            _requests.Add(requestEvent);
            _clientObserver.OnNext(requestEvent);
            return completionSource.Task;
        }

        public IDisposable Subscribe(IObserver<AsyncRequestActionEvent<TResult>> observer)
        {
            _clientObserver = observer;
            return null;
        }
    }
}