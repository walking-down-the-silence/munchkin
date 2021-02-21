using System.Threading.Tasks;

namespace Munchkin.Core.Contracts.PlayerInteraction
{
    public class Response<TResult>
    {
        private readonly TaskCompletionSource<TResult> _taskCompletionSource;

        private Response(TaskCompletionSource<TResult> taskCompletionSource)
        {
            _taskCompletionSource = taskCompletionSource ?? throw new System.ArgumentNullException(nameof(taskCompletionSource));
        }

        public Task<TResult> Task => _taskCompletionSource.Task;

        public static (TaskCompletionSource<TResult>, Response<TResult>) Create()
        {
            TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
            Response<TResult> response = new Response<TResult>(taskCompletionSource);
            return (taskCompletionSource, response);
        }
    }
}
