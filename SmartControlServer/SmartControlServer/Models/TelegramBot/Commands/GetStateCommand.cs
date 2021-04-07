using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Telegram.Bot;
using Telegram.Bot.Types;

namespace SmartControlServer.Models.TelegramBot.Commands
{
    public class GetStateCommand : Command
    {
        public override string Name => "/getState";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            var id = new string(message.Text.Except("/getState ").ToArray());

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
            await client.SendTextMessageAsync(chatId, sensor.State.ToString());
        }
    }
}
