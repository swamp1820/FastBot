using FastBot.Telegram.Interfaces;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FastBot.Telegram.Classes
{
    /// <summary>
    /// Base class for conversations.
    /// </summary>
    /// <typeparam name="T">User state type.</typeparam>
    public abstract class BaseConversation<T> : IConversation<T> where T : UserState, new()
    {
        public BaseConversation(TelegramBotClient client) => Client = client;

        protected TelegramBotClient Client { get; private set; }

        ///<inheritdoc/>
        public abstract Task CheckAnswer(Message message, T userState);

        /// <inheritdoc/>
        public abstract Task AskQuestion(T userState);
    }
}