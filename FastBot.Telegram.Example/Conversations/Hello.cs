using FastBot.Telegram.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FastBot.Telegram.Example.Conversations
{
    public class Hello : IConversation<User>
    {
        public Enum ConversationState => Conv.Hello;

        public User Execute(Message message, User userState)
        {
            Console.WriteLine("sdfsdf");
            return userState;
        }
    }
}
