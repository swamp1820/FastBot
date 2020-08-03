using FastBot.Telegram.Classes;
using FastBot.Telegram.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace FastBot.Telegram
{
    internal class Engine<T> where T : UserState, new()
    {
        private readonly IEnumerable<IConversation<T>> conversations;
        private readonly StateRepository stateRepository;

        public Engine(IEnumerable<IConversation<T>> conversations, StateRepository stateRepository)
        {
            this.conversations = conversations;
            this.stateRepository = stateRepository;
        }

        internal void BotOnMessageReceived(object sender, MessageEventArgs e)
        {
            T user = GetState(e.Message.Chat.Id);
            var conversation = conversations.Where(x => x.ConversationState.ToString() == user.ConversationState.ToString()).FirstOrDefault();
            user = conversation.Execute(e.Message, user);
            SaveState(user);
        }

        private void SaveState(T user)
        {
            stateRepository.Update(user);
        }

        private T GetState(long id)
        {
            var user = stateRepository.Get(id);
            if (user == null)
            {
                user = new T()
                {
                    Id = id,
                };
                stateRepository.Add(user);
            }
            return (T)user;
        }

        internal void BotOnReceiveError(object sender, ReceiveErrorEventArgs e)
        {
            Console.WriteLine("Received error: {0} — {1}",
                e.ApiRequestException.ErrorCode,
                e.ApiRequestException.Message);
        }

        internal void BotOnInlineQuery(object sender, InlineQueryEventArgs e)
        {
            throw new NotImplementedException();
        }

        internal void BotOnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
