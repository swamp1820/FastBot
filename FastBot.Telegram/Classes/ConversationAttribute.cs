using System;

namespace FastBot.Telegram.Classes
{
    public class ConversationAttribute : Attribute
    {
        public string Conversation { get; private set; }
        public StateType Type { get; private set; }

        public ConversationAttribute(string conversationName) : this(conversationName, StateType.None)
        {
        }

        public ConversationAttribute(string conversationName, StateType type)
        {
            Conversation = conversationName;
            Type = type;
        }
    }
}