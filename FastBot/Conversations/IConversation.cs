using FastBot.Messages;
using System.Threading.Tasks;

namespace FastBot.Conversations
{
    public interface IConversation<T>
    {
        /// <summary>
        /// Send conversation question to user.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="userState">User state.</param>
        /// <returns><typeparamref name="T"/> User state.</returns>
        Task OnStateEntered(T userState);

        /// <summary>
        /// Check user answer.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="userState">User state.</param>
        /// <returns><typeparamref name="T"/> User state.</returns>
        Task OnMessageReceived(Message message, T userState);

        /// <summary>
        /// Checks if conversation should be triggered
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="userState">User state.</param>
        /// <returns></returns>
        bool IsTriggered(Message message, T userState);
    }
}