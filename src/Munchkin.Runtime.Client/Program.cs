﻿using Microsoft.Extensions.Logging;
using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using Munchkin.Runtime.Abstractions;
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

            var player = new Player("johny.cash", EGender.Male);

            var table = client.GetGrain<ITable>("table_1");
            var joinRoomResult = await table.JoinAsync(player.Nickname);

            Console.ReadKey();
        }
    }
}
