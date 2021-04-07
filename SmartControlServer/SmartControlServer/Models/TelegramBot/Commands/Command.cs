using System.Threading.Tasks;

using Telegram.Bot;
using Telegram.Bot.Types;

namespace SmartControlServer.Models.TelegramBot.Commands
{
    public abstract class Command
    {
        public abstract string Name { get; }

        public abstract Task Execute(Message message, TelegramBotClient client); //return result

        public bool Contains(string command)
        {
            return command.Contains(this.Name);
            // return command.Contains(this.Name) && command.Contains(AppSettings.Name);
        }
    }
}