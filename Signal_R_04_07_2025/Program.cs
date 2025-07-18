using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace Signal_R_04_07_2025
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // 1️. Create a connection to your SignalR server

            //This creates a SignalR Hub connection object that knows how to 
            //connect your client app to the SignalR Hub running on your server.

            var connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7001/chathub") // Use your hub URL
                .Build();

            // 2️. Set up a handler to receive messages from the server
            connection.On<string>("ReceiveMessage", (message) =>
            {
                Console.WriteLine($"Message from server: {message}");
            });

            // 3️. Start the connection
            await connection.StartAsync();
            Console.WriteLine("Connection started.");

            // 4️. Send messages to the server in a loop

            while (true)
            {
                Console.Write("Enter message: ");
                var input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) break;
                await connection.InvokeAsync("SendMessage", input);
            }

            // 5️. Stop the connection when done
            await connection.StopAsync();
            Console.WriteLine("Connection stopped.");
        }
    }
}