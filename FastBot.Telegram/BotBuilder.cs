using FastBot.Telegram.Classes;
using FastBot.Telegram.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using Telegram.Bot;

namespace FastBot.Telegram
{
    public class BotBuilder<T> where T : UserState, new() 
    {
        private readonly Bot<T> Bot;
        private readonly ServiceCollection collection;

        public BotBuilder()
        {
            Bot = new Bot<T>();
            collection = new ServiceCollection();
            Assembly ConsoleAppAssembly = Assembly.GetEntryAssembly();
            var ConsoleAppTypes =
                from type in ConsoleAppAssembly.GetTypes()
                where !type.IsAbstract
                where typeof(IConversation<T>).IsAssignableFrom(type)
                select type;

            // TODO: Check duplicates
            foreach (var type in ConsoleAppTypes)
            {
                collection.AddTransient(typeof(IConversation<T>), type);
            }
            collection.AddSingleton<Engine<T>>();
        }

        public Bot<T> Build()
        {
            Bot.services = collection.BuildServiceProvider();
            return Bot;
        }

        public BotBuilder<T> AddTelegram(string token)
        {
            Bot.TelegramClient = new TelegramBotClient(token);
            collection.AddSingleton(Bot.TelegramClient);
            return this;
        }

        public BotBuilder<T> AddState()
        {
            collection.AddTransient<StateRepository>();
            return this;
        }
    }
}
