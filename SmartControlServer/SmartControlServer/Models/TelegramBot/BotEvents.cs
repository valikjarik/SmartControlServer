using Telegram.Bot;
using Telegram.Bot.Args;

namespace SmartControlServer.Models.TelegramBot
{
    public static class BotEvents
    {
        internal static async void BotOnMessageReceived(object sender, MessageEventArgs e)
        {
            var client = (TelegramBotClient)sender;
            var commands = Bot.Commands;
            var message = e.Message;

            if (message.Text != null && message.Text != "")
            {
                foreach (var command in commands)
                {
                    if (command.Contains(message.Text))
                    {
                        await command.Execute(message, client);
                        break;
                    }
                }
            }
        }
    }
}