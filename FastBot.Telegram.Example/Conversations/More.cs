using FastBot.Telegram.Classes;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FastBot.Telegram.Example.Conversations
{
    [Conversation(nameof(More))]
    internal class More : BaseConversation<User>
    {
        public More(TelegramBotClient client) : base(client)
        {
        }

        public override async Task AskQuestion(User userState)
        {
            await Client.SendTextMessageAsync(userState.Id, "I'm Fastbot.Telegram");
            await Client.SendTextMessageAsync(userState.Id, "What is your name?");
        }

        public override async Task CheckAnswer(Message message, User userState)
        {
            await Client.SendTextMessageAsync(userState.Id, "GoTo Hello state");
            userState.SetConversationState("Hello");
        }
    }
}