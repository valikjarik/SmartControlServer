using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Telegram.Bot;
using Telegram.Bot.Types;

namespace SmartControlServer.Models.TelegramBot.Commands
{
    public class AddSensorToRuleCommand : Command
    {
        public override string Name => "/addSensorToRule";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            var values = message.Text.Split(' ');

            if (values.Count() < 4)
            {
                await client.SendTextMessageAsync(chatId, "Error: Not all command parameters are set");
                return;
            }

            var ruleId = values[1];
            var sensorId = values[2];

            if (string.IsNullOrEmpty(ruleId))
            {
                await client.SendTextMessageAsync(chatId, "Error: Rule Id is not set");
                return;
            }

            if (string.IsNullOrEmpty(sensorId))
            {
                await client.SendTextMessageAsync(chatId, "Error: Sensor Id is not set");
                return;
            }

            bool value = false;
            if (!bool.TryParse(values[3], out value))
            {
                await client.SendTextMessageAsync(chatId, "Error: Value is not set");
                return;
            }

            var sensor = Data.Sensors.FirstOrDefault(s => s.Id == sensorId);

            if (sensor is null)
            {
                await client.SendTextMessageAsync(chatId, "Error: Sensor not found");
                return;
            }

            var rule = Data.Rules.FirstOrDefault(r => r.Id == ruleId);

            if (rule is null)
            {
                await client.SendTextMessageAsync(chatId, "Error: Rule not found");
                return;
            }

            var ruleSensor = rule.Sensors.FirstOrDefault(s => s.id == sensorId);

            if (!(ruleSensor is (null, false))) ruleSensor.value = value;
            else rule.Sensors.Add((sensorId, value));

            var getRulesCommand = new GetRulesCommand();
            await getRulesCommand.Execute(message, client);
        }
    }
}
