using Microsoft.Extensions.Logging;
using Munchkin.Runtime.Abstractions.GameRoomAggregate;
using Munchkin.Runtime.Abstractions.UserAggregate;
using Orleans;
using Orleans.Configuration;
using System;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Client
{
    internal sealed class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = new ClientBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "munchkin.cluster.development";
                    options.ServiceId = "munchkin";
                })
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                });

            using var client = builder.Build();
            await client.Connect();

            var user = new User(1, "Johny Cash", true);

            var gameRoom = client.GetGrain<IGameRoom>(1);
            var joinRoomResult = await gameRoom.JoinRoom(user);

            Console.ReadKey();
        }
    }
}
