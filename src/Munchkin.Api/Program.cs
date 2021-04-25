using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Munchkin.Runtime.Entities.GameRoomAggregate;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;

namespace Munchkin.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .UseOrleans(siloBuilder =>
            {
                siloBuilder
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
            });
    }
}
