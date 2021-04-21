using Microsoft.Extensions.Logging;
using Munchkin.Runtime.Entities.GameRoomAggregate;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using System;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Silo
{
    internal sealed class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = new SiloHostBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "munchkin.cluster.development";
                    options.ServiceId = "munchkin";
                })
                .AddMemoryGrainStorageAsDefault()
                .ConfigureApplicationParts(parts =>
                {
                    parts.AddApplicationPart(typeof(GameRoom).Assembly).WithReferences();
                })
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                });

            var silo = builder.Build();
            await silo.StartAsync();

            Console.ReadKey();

            await silo.StopAsync();
        }
    }
}
