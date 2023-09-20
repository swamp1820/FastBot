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
    // TODO: refactoring

    /// <summary>
    /// Class that provides conversation flow.
    /// </summary>
    /// <typeparam name="T">User state type.</typeparam>
    internal class Engine<T> where T : UserState, new()
    {
        private readonly IEnumerable<IConversation<T>> _conversations;
        private readonly IRepository<T> _stateRepository;

        public Engine(IEnumerable<IConversation<T>> conversations, IRepository<T> stateRepository = null)
        {
            _conversations = conversations;
            _stateRepository = stateRepository;
        }

        internal async void MessageReceivedAsync(Message message)
        {
            // find user
            T user = GetOrCreateState(message.ChatId, message.ClientType);

            // if we waiting answer from user
            if (_stateRepository is null || user.MustAnswer)
            {
                await GetConversation(user, message).OnMessageReceived(message, user);
                _stateRepository?.Update(user);
            }

            if (!user.MustAnswer || user.StateChanged)
            {
                await Ask(user, message);
            }
        }

        // resolve message handler
        private IConversation<T> GetConversation(T user, Message message)
        {
            // all conversations w/o name are global
            var triggerConversations = _conversations.Where(
                x => ((ConversationAttribute)Attribute.GetCustomAttribute(x.GetType(), typeof(ConversationAttribute)))
                .Name == string.Empty);

            if (triggerConversations.Any())
            {
                var triggerConversation = triggerConversations.Where(x => x.IsTriggered(message, user)).FirstOrDefault();

                if (triggerConversation != null)
                {
                    user.MustAnswer = true;
                    return triggerConversation;
                }
            }

            // conversation with name - resolved by user state
            var stateConversation = _conversations.Where(
                x => ((ConversationAttribute)Attribute.GetCustomAttribute(x.GetType(), typeof(ConversationAttribute)))
                .Name == user.ConversationState)
                .FirstOrDefault();

            if (stateConversation != null)
            {
                return stateConversation;
            }

            // return default conversation
            return _conversations.Where(
              x => ((ConversationAttribute)Attribute.GetCustomAttribute(x.GetType(), typeof(ConversationAttribute)))
              .Type == StateType.Start)
              .FirstOrDefault();
        }

        private async Task Ask(T user, Message message)
        {
            user.StateChanged = false;
            // TODO: exception if conversation null
            await GetConversation(user, message).OnStateEntered(user);

            if (user.StateChanged)
            {
                await Ask(user, message);
                return;
            }

            user.MustAnswer = true;
            _stateRepository?.Update(user);
        }

        private T GetOrCreateState(long id, ClientType clientType)
        {
            var user = _stateRepository?.Get(id);
            if (user == null)
            {
                user = new T()
                {
                    Id = id,
                    Client = clientType,
                };
                Type type = _conversations.Where(
                x => ((ConversationAttribute)Attribute.GetCustomAttribute(x.GetType(), typeof(ConversationAttribute)))
                .Type == StateType.Start)
                .FirstOrDefault()?.GetType();
                user.SetConversationState(type?.Name);
                _stateRepository?.Add(user);
            };

            return user;
        }
    }
}