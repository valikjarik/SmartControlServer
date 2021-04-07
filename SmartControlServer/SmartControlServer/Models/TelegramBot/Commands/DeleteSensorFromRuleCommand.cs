using System.Linq;
using System.Threading.Tasks;

using Telegram.Bot;
using Telegram.Bot.Types;

namespace SmartControlServer.Models.TelegramBot.Commands
{
    public class DeleteSensorFromRuleCommand : Command
    {
        public override string Name => "/deleteSensorFromRule";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            var ruleId = message.Text.Split(' ')[1];
            var sensorId = message.Text.Split(' ')[2];

            var values = message.Text.Split(' ');

            if (values.Count() < 3)
            {
                await client.SendTextMessageAsync(chatId, "Error: Not all command parameters are set");
                return;
            }

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
            var rule = Data.Rules.FirstOrDefault(r => r.Id == ruleId);

            if (rule is null)
            {
                await client.SendTextMessageAsync(chatId, "Error: Rule not found");
                return;
            }

            var sensor = rule.Sensors.FirstOrDefault(s => s.id == sensorId);

            if (sensor is (null, false))
            {
                await client.SendTextMessageAsync(chatId, "Sensor not found");
                return;

            }

            rule.Sensors.Remove(sensor);

            var getRulesCommand = new GetRulesCommand();
            await getRulesCommand.Execute(message, client);
        }
    }
}


