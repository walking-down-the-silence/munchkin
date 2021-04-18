using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using Munchkin.Infrastructure.Repositories;
using Munchkin.Infrastructure.Services;
using Munchkin.Runtime.Entities.Actions;

namespace Munchkin.Infrastructure
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
