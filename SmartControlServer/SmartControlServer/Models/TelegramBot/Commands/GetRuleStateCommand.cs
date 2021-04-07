using System.Linq;
using System.Threading.Tasks;

using Telegram.Bot;
using Telegram.Bot.Types;

namespace SmartControlServer.Models.TelegramBot.Commands
{
    public class GetRuleStateCommand : Command
    {
        public override string Name => "/getRuleState";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            var id = message.Text.Split(" ")[1];

            if (string.IsNullOrEmpty(id))
            {
                await client.SendTextMessageAsync(chatId, "Error: Id is not set");
                return;
            }

            var rule = Data.Rules.FirstOrDefault(s => s.Id == id);

            if (rule is null)
            {
                await client.SendTextMessageAsync(chatId, "Error: Rule is not found");
                return;
            }

            var result = true;

            foreach (var item in rule.Sensors)
            {
                var sensor = Data.Sensors.FirstOrDefault(s => s.Id == item.id);

                if (sensor is null) continue;

                result = result && (sensor.State == item.value);
            }

            await client.SendTextMessageAsync(chatId, result.ToString());
        }
    }
}
