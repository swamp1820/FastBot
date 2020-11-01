using FastBot.Adapters;
using FastBot.Enums;
using FastBot.States;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace FastBot.Core
{
    public class Bot<T> where T : UserState, new()
    {
        internal ServiceProvider services;
        public Clients Clients => services.GetService<Clients>();

        public void Start()
        {
            Clients.StartReceiving();
        }

        public void Stop()
        {
            Clients.StopReceiving();
        }

        public Bot<T> AddTelegram(string token)
        {
            Clients.Adapters.Add(
                ClientType.Telegram,
                new TelegramAdapter<T>(services.GetService<Engine<T>>(), new TelegramBotClient(token)));

            return this;
        }
    }
}