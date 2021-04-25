using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Munchkin.Runtime.Entities.GameRoomAggregate;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using System.Net;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Silo
{
    internal sealed class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                .UseOrleans(siloBuilder =>
                {
                    siloBuilder
                        .UseLocalhostClustering()
                        .Configure<ClusterOptions>(options =>
                        {
                            options.ClusterId = "munchkin.cluster.development";
                            options.ServiceId = "munchkin";
                        })
                        .Configure<EndpointOptions>(options =>
                        {
                            options.AdvertisedIPAddress = IPAddress.Loopback;
                        })
                        .ConfigureApplicationParts(parts =>
                        {
                            parts.AddApplicationPart(typeof(GameRoom).Assembly).WithReferences();
                        })
                        .ConfigureLogging(logging =>
                        {
                            logging.AddConsole();
                        })
                        .AddMemoryGrainStorageAsDefault();
                });


            var host = builder.Build();
            await host.RunAsync();
        }
    }
}
