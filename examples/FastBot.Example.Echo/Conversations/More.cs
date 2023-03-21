﻿using FastBot.Conversations;
using FastBot.Core;
using FastBot.Messages;
using System.Threading.Tasks;

namespace FastBot.Example.Echo.Conversations
{
    [Conversation(nameof(More))]
    internal class More : BaseConversation<User>
    {
        public More(Clients clients) : base(clients)
        {
        }

        public override async Task AskQuestion(User userState)
        {
            await Clients.Send(userState, "I'm Fastbot");
            await Clients.Send(userState, "What is your name?");
        }

        public override async Task CheckAnswer(Message message, User userState)
        {
            userState.Bag["Name"] = message.Text;
            await Clients.Send(userState, $"Hello {userState.Bag.TryGetValue("Name", out object name)}");
            userState.SetConversationState(nameof(Hello));
        }
    }
}