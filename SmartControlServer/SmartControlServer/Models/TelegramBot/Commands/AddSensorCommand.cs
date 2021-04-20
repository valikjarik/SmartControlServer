using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Telegram.Bot;
using Telegram.Bot.Types;

namespace SmartControlServer.Models.TelegramBot.Commands
{
    public class AddSensorCommand : Command
    {
        public override string Name => "/addSensor";

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

            var sensor = Data.Sensors.SingleOrDefault(s => s.Id == id);
            if (sensor is null)
            {
                Data.Sensors.Add(new Sensor { Id = id, State = false });
            }

            var getSensorCommand = new GetSensorsCommand();
            await getSensorCommand.Execute(message, client);
        }
    }
}
