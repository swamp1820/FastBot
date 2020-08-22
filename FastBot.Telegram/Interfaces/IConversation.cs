using FastBot.Telegram.Classes;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace FastBot.Telegram.Interfaces
{
    public interface IConversation<T> where T : UserState
    {
        /// <summary>
        /// Send conversation question to user.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="userState">User state.</param>
        /// <returns><typeparamref name="T"/> User state.</returns>
        Task AskQuestion(T userState);

        /// <summary>
        /// Check user answer.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="userState">User state.</param>
        /// <returns><typeparamref name="T"/> User state.</returns>
        Task CheckAnswer(Message message, T userState);
    }
}