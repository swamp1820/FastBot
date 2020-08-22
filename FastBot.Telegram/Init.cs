using FastBot.Telegram.Classes;
using FastBot.Telegram.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using Telegram.Bot;

namespace FastBot.Telegram
{
    /// <summary>
    /// Bot class <see cref="Bot{T}"/.
    /// </summary>
    /// <typeparam name="T">User state type.</typeparam>
    public class Bot<T> where T : UserState, new()
    {
        /// <summary>
        /// Initialize bot.
        /// </summary>
        /// <param name="botKey">Telegram bot API key.</param>
        public static void Init(string botKey)
        {
            var collection = new ServiceCollection();
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
            collection.AddTransient<StateRepository>();
            Client = new TelegramBotClient(botKey);
            collection.AddSingleton(Client);
            var serviceProvider = collection.BuildServiceProvider();

            var engine = serviceProvider.GetService<Engine<T>>();
            Client.OnMessage += engine.BotOnMessageReceivedAsync;
            Client.OnReceiveError += engine.BotOnReceiveError;
            Client.OnInlineQuery += engine.BotOnInlineQuery;
            Client.OnCallbackQuery += engine.BotOnCallbackQuery;
            Client.StartReceiving();
        }

        /// <summary>
        /// Telegram bot client.
        /// </summary>
        public static TelegramBotClient Client { get; private set; }
    }
}