using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Telegram.Bot;
using Telegram.Bot.Types;

namespace SmartControlServer.Models.TelegramBot.Commands
{
    public class AddRuleCommand : Command
    {
        public override string Name => "/addRule";

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

            var rule = Data.Rules.SingleOrDefault(s => s.Id == id);
            if (rule is null)
            {
                Data.Rules.Add(new Rule(id));
            }

            var getRulesCommand = new GetRulesCommand();
            await getRulesCommand.Execute(message, client);
        }
    }
}
