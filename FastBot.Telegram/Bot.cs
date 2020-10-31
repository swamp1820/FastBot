using FastBot.Telegram.Classes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace FastBot.Telegram
{
    public class Bot<T> where T : UserState, new()
    {
        internal ServiceProvider services;
        private Engine<T> Engine => services.GetService<Engine<T>>();
        public TelegramBotClient TelegramClient { get; internal set; }
        public void Start()
        {
            TelegramClient.OnMessage += Engine.BotOnMessageReceivedAsync;
            TelegramClient.OnReceiveError += Engine.BotOnReceiveError;
            TelegramClient.StartReceiving();
        }

        public void Stop()
        {
            TelegramClient.OnMessage -= Engine.BotOnMessageReceivedAsync;
            TelegramClient.OnReceiveError -= Engine.BotOnReceiveError;
            TelegramClient.StopReceiving();
        }
    }
}
