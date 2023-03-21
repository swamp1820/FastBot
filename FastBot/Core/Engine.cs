using FastBot.Conversations;
using FastBot.Enums;
using FastBot.Messages;
using FastBot.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastBot.Core
{
    /// <summary>
    /// Class that provides conversation flow.
    /// </summary>
    /// <typeparam name="T">User state type.</typeparam>
    internal class Engine<T> where T : UserState, new()
    {
        private readonly IEnumerable<IConversation<T>> conversations;
        private readonly IRepository<T> stateRepository;

        public Engine(IEnumerable<IConversation<T>> conversations, IRepository<T> stateRepository)
        {
            this.conversations = conversations;
            this.stateRepository = stateRepository;
        }

        internal async void MessageReceivedAsync(Message message)
        {
            T user = GetOrCreateState(message.ChatId, message.ClientType);

            var c = GetConversation(user.ConversationState);
            if (user.MustAnswer)
            {
                await c.CheckAnswer(message, user);
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
                .FirstOrDefault() ?? conversations.Where(
                x => ((ConversationAttribute)Attribute.GetCustomAttribute(x.GetType(), typeof(ConversationAttribute)))
                .Type == StateType.Start)
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

        private T GetOrCreateState(long id, ClientType clientType)
        {
            var user = stateRepository.Get(id);
            if (user == null)
            {
                user = new T()
                {
                    Id = id,
                    Client = clientType,
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
    }
}