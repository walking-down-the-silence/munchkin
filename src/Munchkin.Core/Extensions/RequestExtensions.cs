using MediatR;
using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using Munchkin.Extensions.Threading;
using System.Threading.Tasks;

namespace Munchkin.Core.Extensions
{
    public static class RequestExtensions
    {
        public static Task<TResult> SendAsync<TResult>(this IRequest<Response<TResult>> request, Table table)
        {
            return table.RequestSink.Send(request).SelectMany(x => x.Task);
        }
    }
}
