﻿using Microsoft.Extensions.DependencyInjection;
using Munchkin.Core.Contracts;
using Munchkin.Core.Model;

namespace Munchkin.Expansion.Deluxe
{
    public static class MunchkinDeluxeModule
    {
        public static IServiceCollection AddMunchkinDeluxe(this IServiceCollection services)
        {
            return services
                .AddTransient<IExpansion, MunchkinDeluxeExpansion>();
        }
    }
}