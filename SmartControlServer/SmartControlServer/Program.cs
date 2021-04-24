using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using SmartControlServer.Models;
using SmartControlServer.Models.TelegramBot;

using System;

namespace SmartControlServer
{
    public class Program
    {
        public static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Token was not set as argument");
                Console.ResetColor();
                return 1;
            }

            AppSettings.Token = args[0];
            Console.WriteLine($"Token: {AppSettings.Token}");

            var client = Bot.Get();
            client.OnMessage += BotEvents.BotOnMessageReceived;
            client.StartReceiving();

            CreateHostBuilder(args).Build().Run();
            return 0;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}