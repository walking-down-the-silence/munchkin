using Microsoft.Extensions.DependencyInjection;
using Munchkin.Runtime.Abstractions;
using Munchkin.Runtime.Abstractions.GameRoomAggregate;
using Munchkin.Runtime.Abstractions.UserAggregate;
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
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<IPlayerActionRepository, PlayerActionRepository>()
                .AddTransient<IGameRoomRepository, GameRoomRepository>()
                .AddTransient<IGameEngineRepository, GameEngineRepository>()
                .AddTransient<GameRoomService>()
                .AddTransient<GameEngineService>();
        }
    }
}
