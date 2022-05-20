using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Munchkin.Runtime.Abstractions;
using Munchkin.Runtime.Repositories;
using Munchkin.Runtime.Services;

namespace Munchkin.Runtime
{
    public static class MunchkinRuntimeModule
    {
        public static IServiceCollection AddMunchkinGameServices(this IServiceCollection services)
        {
            return services
                .AddMediatR(typeof(MunchkinRuntimeModule))
                .AddScoped<DungeonService>()
                .AddScoped<PlayerService>()
                .AddScoped<TableService>()
                .AddScoped<CharityService>()
                .AddScoped<CombatService>()
                .AddScoped<RunningAwayService>()
                .AddScoped<CurseService>();
        }

        public static IServiceCollection AddMunchkinInMemoryPersistance(this IServiceCollection services)
        {
            return services
                .AddSingleton<IExpansionsProvider, ExpansionsProvider>()
                .AddSingleton<IPlayerRepository, InMemoryPlayerRepository>()
                .AddSingleton<ITableRepository, InMemoryTableRepository>()
                .AddSingleton<ITradeRepository, InMemoryTradeRepository>();
        }
    }
}
