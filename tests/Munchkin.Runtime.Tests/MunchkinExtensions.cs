using Microsoft.Extensions.DependencyInjection;
using Munchkin.Core.Contracts;

namespace Munchkin.Runtime.Tests
{
    internal static class MunchkinExtensions
    {
        public static IServiceCollection AddMunchkinDeluxeTest(this IServiceCollection services)
        {
            return services
                .AddTransient<IExpansion, MunchkinDeluxeOrderedExpansion>();
        }
    }
}
