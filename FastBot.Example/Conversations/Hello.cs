using System.Collections.Generic;
using System.Threading.Tasks;
using FastBot.Core;
using FastBot.Conversations;
using FastBot.Enums;
using FastBot.Messages;

namespace FastBot.Telegram.Example.Conversations
{
    [Conversation(nameof(Hello), StateType.Start)]
    public class Hello : BaseConversation<User>
    {
        private readonly Keyboard yNKeyboard;

        public Hello(Clients clients) : base(clients) => yNKeyboard = new Keyboard()
        {
            Buttons = new List<List<Button>>()
                {
                    new List<Button>(new List<Button>
                    {
                        new Button("Yes"),
                        new Button("No"),
                    })
                },
            Resize = true,
            OneTime = true,
        };

        public override async Task AskQuestion(User userState)
        {
            await Clients.Send(userState, "Hi, I'm FastBot.Telegram");
            await Clients.Send(userState, "Do you want to know about me more?", yNKeyboard);
        }

        public override async Task CheckAnswer(Message message, User userState)
        {
            switch (message.Text)
            {
                case "Yes":
                    userState.SetConversationState(nameof(More));
                    break;

                case "No":
                    userState.SetConversationState(nameof(Echo));
                    break;

                default:
                    await Clients.Send(userState, "Choose the button", yNKeyboard);
                    break;
            }
        }
    }
}