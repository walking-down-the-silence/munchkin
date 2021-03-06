﻿using MediatR;
using Munchkin.Core.Contracts.PlayerInteraction;
using Munchkin.Core.Model;
using System.Threading.Tasks;

namespace Munchkin.Core.Extensions
{
    public static class RequestExtensions
    {
        public static async Task<TResult> SendAsync<TResult>(this IRequest<Response<TResult>> request, Table table)
        {
            var response = await table.RequestSink.Send(request);
            var result = await response.Task;
            return result;
        }
    }
}
