using FastBot.Conversations;
using FastBot.Core;
using FastBot.Messages;
using System.Threading.Tasks;


namespace FastBot.Example.Global.Conversations
{
    [Conversation]
    internal class Weather : BaseConversation<User>
    {
        public Weather(Clients clients) : base(clients)
        {
        }

        public override bool IsTriggered(Message message, User userState) => message.CompareText("weather"); 
        
        public override async Task OnMessageReceived(Message message, User userState)
        {
           await Clients.Send(userState, "weather is fine, thanks");
        }
    }
}