using Munchkin.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Core.PlayerInteraction
{
    public class AsyncRequestActionEvent<TResult> : IReplyable<TResult>
    {
        private readonly List<AsyncRequestActionEvent<TResult>> _requests;
        private readonly TaskCompletionSource<TResult> _completionSource;

        public AsyncRequestActionEvent(
            List<AsyncRequestActionEvent<TResult>> requests,
            TaskCompletionSource<TResult> completionSource)
        {
            _requests = requests ?? throw new ArgumentNullException(nameof(requests));
            _completionSource = completionSource ?? throw new ArgumentNullException(nameof(completionSource));
        }

        public void Cancel()
        {
            _completionSource.SetCanceled();
            _requests.Remove(this);
        }

        public void Reply(TResult result)
        {
            _completionSource.SetResult(result);
            _requests.Remove(this);
        }
    }
}