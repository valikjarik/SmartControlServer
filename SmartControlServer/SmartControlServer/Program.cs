using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using SmartControlServer.Models.TelegramBot;

namespace SmartControlServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var client = Bot.Get();
            client.OnMessage += BotEvents.BotOnMessageReceived;
            client.StartReceiving();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}