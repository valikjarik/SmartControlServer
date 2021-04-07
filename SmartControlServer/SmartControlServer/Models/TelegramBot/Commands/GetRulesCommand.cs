using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Telegram.Bot;
using Telegram.Bot.Types;

namespace SmartControlServer.Models.TelegramBot.Commands
{
    public class GetRulesCommand : Command
    {
        public override string Name => "/rules";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            var rules = Data.Rules;

            if (!rules.Any())
            {
                await client.SendTextMessageAsync(chatId, "Rules list is empty");
                return;
            }

            var response = "Rules: \n";

            foreach (var item in rules)
            {
                if (item.Sensors.Count > 0)
                {
                    var sensors = item.Sensors.Select(x => $"(Id: {x.id} Value: {x.value})").Aggregate((f, s) => f + "  +  " + s);
                    response = response + $"- Id: {item.Id}  Expression:  {sensors}  = true \n";
                }
                else
                {
                    response = response + $"- Id: {item.Id}   \n";
                }
            }

            await client.SendTextMessageAsync(chatId, response);
        }
    }
}
