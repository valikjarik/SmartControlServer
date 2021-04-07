using System.Linq;
using System.Threading.Tasks;

using Telegram.Bot;
using Telegram.Bot.Types;

namespace SmartControlServer.Models.TelegramBot.Commands
{
    public class DeleteSensorCommand : Command
    {
        public override string Name => "/deleteSensor";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            var id = message.Text.Split(' ')[1];

            if (string.IsNullOrEmpty(id))
            {
                await client.SendTextMessageAsync(chatId, "Error: Id is not set");
                return;
            }

            var sensor = Data.Sensors.FirstOrDefault(s => s.Id == id);
            if (sensor is null)
            {
                await client.SendTextMessageAsync(chatId, "Error: Sensor not found");
                return;
            }

            Data.Sensors.Remove(sensor);

            var getSensorCommand = new GetSensorsCommand();
            await getSensorCommand.Execute(message, client);
        }
    }
}
