using Microsoft.Extensions.DependencyInjection;
using Munchkin.Runtime.Entities.Actions;
using Munchkin.Services.Lobby.Repositories;
using Munchkin.Services.Lobby.Services;

namespace Munchkin.Services.Lobby
{
    public static class MunchkinLobbyModule
    {
        public static IServiceCollection AddMunchkinGameServices(this IServiceCollection services)
        {
            return services
                .AddTransient<IPlayerActionRepository, PlayerActionRepository>()
                .AddTransient<GameEngineService>()
                .AddTransient<GameRoomService>();
        }
    }
}
