using FastBot.Conversations;
using FastBot.Core;
using FastBot.Messages;
using System.Threading.Tasks;


namespace FastBot.Example.Global.Conversations
{
    [Conversation]
    internal class Search : BaseConversation<User>
    {
        public Search(Clients clients) : base(clients)
        {
        }

        public override bool IsTriggered(Message message, User userState) => message.CompareText("search"); 
        
        public override async Task OnMessageReceived(Message message, User userState)
        {
           await Clients.Send(userState, "try Google instead!");
        }
    }
}