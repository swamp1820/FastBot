using FastBot.Telegram.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace FastBot.Telegram.Example.Conversations
{
    [Conversation(nameof(Hello), StateType.Start)]
    public class Hello : BaseConversation<User>
    {
        private readonly ReplyKeyboardMarkup yNKeyboard;

        public Hello(TelegramBotClient client) : base(client) => yNKeyboard = new ReplyKeyboardMarkup()
        {
            Keyboard = new List<List<KeyboardButton>>()
                {
                    new List<KeyboardButton>(new List<KeyboardButton>
                    {
                        new KeyboardButton("Yes"),
                        new KeyboardButton("No"),
                    })
                },
            ResizeKeyboard = true,
            OneTimeKeyboard = true,
        };

        public override async Task AskQuestion(User userState)
        {
            await Client.SendTextMessageAsync(userState.Id, "Hi, I'm FastBot.Telegram");
            await Client.SendTextMessageAsync(userState.Id, "Do you want to know about me more?", replyMarkup: yNKeyboard);
        }

        public override async Task CheckAnswer(Message message, User userState)
        {
            switch (message.Text)
            {
                case "Yes":
                    userState.SetConversationState("More");
                    break;

                case "No":
                    userState.SetConversationState("Echo");
                    break;

                default:
                    await Client.SendTextMessageAsync(userState.Id, "Choose the button", replyMarkup: yNKeyboard);
                    break;
            }
        }
    }
}