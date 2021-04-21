using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using Munchkin.Runtime.Client.Repositories;
using Munchkin.Runtime.Client.Services;
using Munchkin.Runtime.Entities.Actions;

namespace Munchkin.Runtime.Client
{
    public static class MunchkinOriginalModule
    {
        public static IServiceCollection AddMunchkinDeluxe(this IServiceCollection services)
        {
            return services
                .AddMediatR(typeof(MunchkinOriginalModule))
                .AddTransient<IPlayerActionRepository, PlayerActionRepository>()
                .AddTransient<GameEngineService>()
                .AddTransient<GameRoomService>()
                .AddTransient<IExpansion, MunchkinOriginal>();
        }
    }
}
