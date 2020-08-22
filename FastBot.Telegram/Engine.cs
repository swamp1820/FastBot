using FastBot.Telegram.Classes;
using FastBot.Telegram.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Args;

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

        internal async void BotOnMessageReceivedAsync(object sender, MessageEventArgs e)
        {
            T user = GetOrCreateState(e.Message.Chat.Id);

            var c = GetConversation(user.ConversationState);
            if (user.MustAnswer)
            {
                await c.CheckAnswer(e.Message, user);
                stateRepository.Update(user);
            }

            if (!user.MustAnswer || user.StateChanged)
            {
                await Ask(user);
            }
        }

        private IConversation<T> GetConversation(string conversation)
        {
            return conversations.Where(
                x => ((ConversationAttribute)Attribute.GetCustomAttribute(x.GetType(), typeof(ConversationAttribute)))
                .Conversation == conversation)
                .FirstOrDefault();
        }

        private async Task Ask(T user)
        {
            user.StateChanged = false;
            var c = GetConversation(user.ConversationState);
            await c.AskQuestion(user);

            if (user.StateChanged)
            {
                await Ask(user);
                return;
            }

            user.MustAnswer = true;
            stateRepository.Update(user);
        }

        private T GetOrCreateState(long id)
        {
            var user = stateRepository.Get(id);
            if (user == null)
            {
                user = new T()
                {
                    Id = id,
                };
                Type type = conversations.Where(
                x => ((ConversationAttribute)Attribute.GetCustomAttribute(x.GetType(), typeof(ConversationAttribute)))
                .Type == StateType.Start)
                .FirstOrDefault().GetType();
                user.SetConversationState(type.Name);
                stateRepository.Add(user);
            };

            return (T)user;
        }

        internal void BotOnReceiveError(object sender, ReceiveErrorEventArgs e)
        {
            Console.WriteLine("Received error: {0} — {1}",
                e.ApiRequestException.ErrorCode,
                e.ApiRequestException.Message);
        }

        // TODO: implement
        internal void BotOnInlineQuery(object sender, InlineQueryEventArgs e)
        {
            throw new NotImplementedException();
        }

        // TODO: implement
        internal void BotOnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}