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
            commandsList = new List<Command>();
            commandsList.Add(new HelloCommand());
            commandsList.Add(new GetSensorsCommand());
            commandsList.Add(new GetStateCommand());
            commandsList.Add(new SetStateCommand());
            commandsList.Add(new GetRulesCommand());
            commandsList.Add(new GetRuleStateCommand());
            commandsList.Add(new AddRuleCommand());
            commandsList.Add(new AddSensorToRuleCommand());
            commandsList.Add(new AddSensorCommand());
            commandsList.Add(new DeleteSensorFromRuleCommand());
            commandsList.Add(new DeleteSensorCommand());

            client = new TelegramBotClient(AppSettings.Key);

            return client;
        }
    }
}