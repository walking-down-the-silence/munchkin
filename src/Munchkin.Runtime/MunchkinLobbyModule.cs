using Microsoft.Extensions.DependencyInjection;
using Munchkin.Runtime.Abstractions;
using Munchkin.Runtime.Abstractions.Actions;
using Munchkin.Runtime.Repositories;
using Munchkin.Runtime.Services;

namespace Munchkin.Runtime
{
    public static class MunchkinLobbyModule
    {
        public static IServiceCollection AddMunchkinGameServices(this IServiceCollection services)
        {
            return services
                .AddSingleton<IPlayerRepository, PlayerRepository>()
                .AddTransient<IPlayerActionRepository, PlayerActionRepository>()
                .AddTransient<TableService>()
                .AddTransient<PlayerService>();
        }
    }
}
