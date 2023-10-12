using System;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace SignalRClient // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/requesthub")
                .WithAutomaticReconnect()
                .Build();

            connection.Reconnecting += (sender) =>
            {
                Console.WriteLine("Attempting to reconnect...");

                return Task.CompletedTask;
            };

            connection.Reconnected += (sender) =>
            {
                Console.WriteLine("Reconnected to the server.");

                return Task.CompletedTask;
            };

            connection.Closed += (sender) =>
            {
                Console.WriteLine("Connection closed.");

                return Task.CompletedTask;
            };

            connection.On<double?, double?, string?>("SendRequestWithCategory", (latitude, longitude, category) =>
            {
                Console.WriteLine($"Requested latitude: {latitude}, longitude: {longitude}, category: {category}");
                return Task.CompletedTask;
            });

            connection.On<double?, double?>("SendRequest", (latitude, longitude) =>
            {
                Console.WriteLine($"Requested latitude: {latitude}, longitude: {longitude}");
                return Task.CompletedTask;
            });

            try
            {
                await connection.StartAsync();
                Console.WriteLine("Connection started.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}