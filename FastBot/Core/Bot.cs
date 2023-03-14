using FastBot.Adapters;
using FastBot.Enums;
using FastBot.States;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using VkNet;
using VkNet.Model;

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

        public Bot<T> AddVk(string token, ulong groupId)
        {
            var client = new VkApi();
            client.Authorize(new ApiAuthParams() { AccessToken = token });

            Clients.Adapters.Add(
                ClientType.Vk,
                new VkAdapter<T>(services.GetService<Engine<T>>(), client, groupId));

            return this;
        }

        public Bot<T> AddCustomAdapter(IAdapter adapter)
        {
            Clients.Adapters.Add(
                ClientType.Custom, adapter);

            return this;
        }
    }
}