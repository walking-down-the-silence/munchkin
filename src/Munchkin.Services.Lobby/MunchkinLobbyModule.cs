﻿using Microsoft.Extensions.DependencyInjection;
using Munchkin.Runtime.Abstractions.Actions;
using Munchkin.Runtime.Abstractions.Tables;
using Munchkin.Runtime.Abstractions.UserAggregate;
using Munchkin.Services.Lobby.Repositories;
using Munchkin.Services.Lobby.Services;

namespace Munchkin.Services.Lobby
{
    public static class MunchkinLobbyModule
    {
        public static IServiceCollection AddMunchkinGameServices(this IServiceCollection services)
        {
            return services
                .AddSingleton<IPlayerRepository, PlayerRepository>()
                .AddTransient<IPlayerActionRepository, PlayerActionRepository>()
                .AddTransient<ITableRepository, TableRepository>()
                .AddTransient<TableService>()
                .AddTransient<PlayerService>();
        }
    }
}
