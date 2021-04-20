using SmartControlServer.Models.TelegramBot.Commands;

using System.Collections.Generic;

using Telegram.Bot;

namespace SmartControlServer.Models.TelegramBot
{
    public static class Bot
    {
        private static TelegramBotClient client;
        private static List<Command> commandsList;

        public static IReadOnlyList<Command> Commands { get => commandsList.AsReadOnly(); }

        public static TelegramBotClient Get()
        {
            if (client != null)
            {
                return client;
            }

            commandsList = new List<Command>
            {
                new HelloCommand(),
                new GetSensorsCommand(),
                new GetStateCommand(),
                new SetStateCommand(),
                new GetRulesCommand(),
                new GetRuleStateCommand(),
                new AddRuleCommand(),
                new AddSensorToRuleCommand(),
                new AddSensorCommand(),
                new DeleteSensorFromRuleCommand(),
                new DeleteSensorCommand()
            };

            client = new TelegramBotClient(AppSettings.Key);

            return client;
        }
    }
}