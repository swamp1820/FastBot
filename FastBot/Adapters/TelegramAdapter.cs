using FastBot.Core;
using FastBot.Enums;
using FastBot.Messages;
using FastBot.States;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace FastBot.Adapters
{
    internal class TelegramAdapter<T> : BaseAdapter<T>, IAdapter where T : UserState, new()
    {
        internal TelegramBotClient telegramBotClient;

        public TelegramAdapter(Engine<T> engine, TelegramBotClient TelegramBotClient) : base(engine)
        {
            telegramBotClient = TelegramBotClient;
            telegramBotClient.OnMessage += OnMessageReceivedAsync;
        }

        internal void OnMessageReceivedAsync(object sender, MessageEventArgs e)
        {
            var message = new Message()
            {
                ClientType = ClientType.Telegram,
                ChatId = e.Message.Chat.Id,
                Text = e.Message.Text
            };

            Engine.MessageReceivedAsync(message);
        }

        public Task SendTextMessageAsync(long id, string message)
        {
            return telegramBotClient.SendTextMessageAsync(id, message);
        }

        public void Start()
        {
            telegramBotClient.StartReceiving();
        }

        public void Stop()
        {
            telegramBotClient.StopReceiving();
        }

        public Task SendTextMessageAsync(long id, string message, Keyboard keyboard)
        {
            var bt = new List<List<KeyboardButton>>();
            foreach (var row in keyboard.Buttons.ToList())
            {
                var r = new List<KeyboardButton>();
                foreach (var button in row.ToList())
                {
                    r.Add(new KeyboardButton(button.Text));
                }

                bt.Add(r);
            }
            var kb = new ReplyKeyboardMarkup()
            {
                Keyboard = bt,
                ResizeKeyboard = keyboard.Resize,
                OneTimeKeyboard = keyboard.OneTime,
            };

            return telegramBotClient.SendTextMessageAsync(id, message, replyMarkup: kb);
        }
    }
}