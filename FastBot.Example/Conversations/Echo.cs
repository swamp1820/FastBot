﻿using FastBot.Conversations;
using FastBot.Core;
using FastBot.Messages;
using System.Threading.Tasks;


namespace FastBot.Telegram.Example.Conversations
{
    [Conversation(nameof(Echo))]
    internal class Echo : BaseConversation<User>
    {
        public Echo(Clients clients) : base(clients)
        {
        }

        public override async Task AskQuestion(User userState)
        {
            await Clients.Send(userState, "Ok, i will repeat everything you say. Say \"exit\" or \"quit\" for reseting.");
        }

        public override async Task CheckAnswer(Message message, User userState)
        {
            if (!message.CompareText("exit") && !message.CompareText("quit"))
            {
                await Clients.Send(userState, message.Text);
            }
            else
            {
                userState.SetConversationState(nameof(Hello)); 
            }

        }
    }
}