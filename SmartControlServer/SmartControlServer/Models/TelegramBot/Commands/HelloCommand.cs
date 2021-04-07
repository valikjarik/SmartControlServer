using System.Threading.Tasks;

using Telegram.Bot;
using Telegram.Bot.Types;

namespace SmartControlServer.Models.TelegramBot.Commands
{
    public class HelloCommand : Command
    {
        public override string Name => "/start";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            await client.SendTextMessageAsync(chatId, "Hi!", replyToMessageId: messageId);
        }
    }
}