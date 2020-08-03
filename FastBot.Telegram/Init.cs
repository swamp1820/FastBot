using FastBot.Telegram.Classes;
using FastBot.Telegram.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Telegram.Bot;

namespace FastBot.Telegram
{
    public class Bot<T> where T : UserState, new()
    {
        public static void Init(string botKey)
        {
            //собираем беседы в контейнер
            var collection = new ServiceCollection();
            Assembly ConsoleAppAssembly = Assembly.GetEntryAssembly();
            var ConsoleAppTypes =
                from type in ConsoleAppAssembly.GetTypes()
                where !type.IsAbstract
                where typeof(IConversation<T>).IsAssignableFrom(type)
                select type;

            foreach (var type in ConsoleAppTypes)
            {
                collection.AddTransient(typeof(IConversation<T>), type);
            }


            collection.AddSingleton<Engine<T>>();
            collection.AddTransient<StateRepository>();
            var serviceProvider = collection.BuildServiceProvider();

            var client = new TelegramBotClient(botKey);
            var engine = serviceProvider.GetService<Engine<T>>();
            client.OnMessage += engine.BotOnMessageReceived;
            client.OnReceiveError += engine.BotOnReceiveError;
            client.OnInlineQuery += engine.BotOnInlineQuery;
            client.OnCallbackQuery += engine.BotOnCallbackQuery;
            client.StartReceiving();
        }
    }
}
