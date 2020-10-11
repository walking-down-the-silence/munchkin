using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using System;
using System.Collections.Generic;

namespace Munchkin.Core.PlayerInteraction
{
    public class ReactiveRequestSink<TResult> : IReactiveRequestable<TResult>
    {
        private readonly List<ReactiveRequestActionEvent<TResult>> _requests = new List<ReactiveRequestActionEvent<TResult>>();
        private IObserver<ReactiveRequestActionEvent<TResult>> _clientObserver;
        private IObserver<TResult> _sourceObserver;

        public void RequestAsync(Player target, Type requestedDataType)
        {
            var requestEvent = new ReactiveRequestActionEvent<TResult>(_requests, _sourceObserver, target, requestedDataType);
            _clientObserver.OnNext(requestEvent);
        }

        public IDisposable Subscribe(IObserver<TResult> observer)
        {
            _sourceObserver = observer;
            return null;
        }

        public IDisposable Subscribe(IObserver<ReactiveRequestActionEvent<TResult>> observer)
        {
            _clientObserver = observer;
            return null;
        }
    }
}