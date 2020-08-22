using FastBot.Telegram.Classes;
using System.Threading.Tasks;
using Telegram.Bot;

using Telegram.Bot.Types;

namespace FastBot.Telegram.Example.Conversations
{
    [Conversation(nameof(Echo))]
    internal class Echo : BaseConversation<User>
    {
        public Echo(TelegramBotClient client) : base(client)
        {
        }

        public override async Task AskQuestion(User userState)
        {
            await Client.SendTextMessageAsync(userState.Id, "Ok, i will repeat everything you say");
        }

        public override async Task CheckAnswer(Message message, User userState)
        {
            await Client.SendTextMessageAsync(userState.Id, message.Text);
        }
    }
}