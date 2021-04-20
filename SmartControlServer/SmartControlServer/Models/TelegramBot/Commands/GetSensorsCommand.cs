using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SmartControlServer.Models.TelegramBot.Commands
{
    public class GetSensorsCommand : Command
    {
        public override string Name => "/sensors";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var sensors = Data.Sensors;

            if (!sensors.Any())
            {
                await client.SendTextMessageAsync(chatId, "Sensors list is empty");
                return;
            }

            var response = "Sensors: \n";

            foreach (var item in sensors)
            {
                response = response + $"Id: {item.Id},  State: {item.State} \n";
            }

            await client.SendTextMessageAsync(chatId, response);
        }
    }
}
