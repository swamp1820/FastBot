using FastBot.Conversations;
using FastBot.Core;
using FastBot.Messages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastBot.Example.Echo.Conversations
{
    [Conversation(nameof(More))]
    internal class More : BaseConversation<User>
    {
        public More(Clients clients) : base(clients)
        {
        }

        public override async Task OnStateEntered(User userState)
        {
            await Clients.Send(userState, "I'm Fastbot");
            await Clients.Send(userState, "What is your name?");
        }

        public override async Task OnMessageReceived(Message message, User userState)
        {
            userState.Bag["Name"] = message.Text;
            await Clients.Send(userState, $"Hello {userState.Bag.GetValueOrDefault("Name")}");
            userState.SetConversationState(nameof(Hello));
        }
    }
}