using MediatR;
using Munchkin.Core.Contracts.PlayerInteraction;
using Munchkin.Core.Model;
using System.Threading.Tasks;

namespace Munchkin.Core.Extensions
{
    public static class RequestExtensions
    {
        public static async Task<T> SendRequestAsync<T>(this IRequest<Response<T>> request, Table state)
        {
            var response = await state.RequestSink.Send(request);
            return await response.Task;
        }
    }
}
