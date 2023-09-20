using FastBot.Core;
using FastBot.Messages;
using FastBot.States;
using System.Threading.Tasks;

namespace FastBot.Conversations
{
    /// <summary>
    /// Base class for conversations.
    /// </summary>
    /// <typeparam name="T">User state type.</typeparam>
    public abstract class BaseConversation<T> : IConversation<T> where T : UserState, new()
    {
        public BaseConversation(Clients clients) => Clients = clients;

        protected Clients Clients { get; private set; }

        /// <inheritdoc/>
        public virtual bool IsTriggered(Message message, T userState) => false;

        /// <inheritdoc/>
        public abstract Task OnMessageReceived(Message message, T userState);

        /// <inheritdoc/>
        public virtual Task OnStateEntered(T userState) => Task.CompletedTask;
    }
}