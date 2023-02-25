using FastBot.Conversations;
using FastBot.Core;
using FastBot.Messages;
using System.Threading.Tasks;

namespace FastBot.Telegram.Example.Conversations
{
    [Conversation(nameof(More))]
    internal class More : BaseConversation<User>
    {
        public More(Clients clients) : base(clients)
        {
        }

        public override async Task AskQuestion(User userState)
        {
            await Clients.Send(userState, "I'm Fastbot.Telegram");
            await Clients.Send(userState, "What is your name?");
        }

        public override async Task CheckAnswer(Message message, User userState)
        {
            await Clients.Send(userState, "GoTo Hello state");
            userState.SetConversationState(nameof(Hello));
        }
    }
}