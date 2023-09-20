using FastBot.Conversations;
using FastBot.States;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace FastBot.Core
{
    public class BotBuilder<T> where T : UserState, new()
    {
        private readonly Bot<T> Bot;
        private readonly ServiceCollection collection;

        public BotBuilder()
        {
            Bot = new Bot<T>();
            collection = new ServiceCollection();
            collection.AddSingleton(new Clients());
            collection.AddSingleton<Engine<T>>();
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
        }

        public Bot<T> Build()
        {
            Bot.services = collection.BuildServiceProvider();
            return Bot;
        }

        public BotBuilder<T> UseState()
        {
            return UseState(typeof(BaseRepository<T>));
        }

        public BotBuilder<T> UseState(Type type)
        {
            collection.AddTransient(typeof(IRepository<T>), type);
            return this;
        }
    }
}