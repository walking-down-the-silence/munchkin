using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using System;
using System.Collections.Generic;

namespace Munchkin.Core.PlayerInteraction
{
    public class ReactiveRequestActionEvent<TResult> : IReplyable<TResult>
    {
        private readonly List<ReactiveRequestActionEvent<TResult>> _requests;
        private readonly IObserver<TResult> _sourceObserver;

        public ReactiveRequestActionEvent(
            List<ReactiveRequestActionEvent<TResult>> requests,
            IObserver<TResult> sourceObserver,
            Player targetPlayer,
            Type requestedDataType)
        {
            _requests = requests ?? throw new ArgumentNullException(nameof(requests));
            _sourceObserver = sourceObserver ?? throw new ArgumentNullException(nameof(sourceObserver));
            TargetPlayer = targetPlayer ?? throw new ArgumentNullException(nameof(targetPlayer));
            RequestedDataType = requestedDataType ?? throw new ArgumentNullException(nameof(requestedDataType));
        }

        public Player TargetPlayer { get; }

        public Type RequestedDataType { get; }

        public void Cancel()
        {
            _sourceObserver.OnNext(default);
            _requests.Remove(this);
        }

        public void Reply(TResult result)
        {
            _sourceObserver.OnNext(result);
            _requests.Remove(this);
        }
    }
}