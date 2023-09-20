using FastBot.Enums;
using System;

namespace FastBot.Conversations
{
    /// <summary>
    /// Atrtribute that provides conversation metadata.
    /// </summary>
    public class ConversationAttribute : Attribute
    {
        /// <summary>
        /// Conversation name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Conversation type.
        /// </summary>
        public StateType Type { get; private set; }

        /// <summary>
        /// Initialize new instance of <see cref="ConversationAttribute"/>.
        /// </summary>
        /// <param name="conversationName">Name of conversation. Must be unique.</param>
        public ConversationAttribute(string conversationName) : this(conversationName, StateType.None)
        {
        }

        /// <summary>
        /// Initialize new instance of <see cref="ConversationAttribute"/>.
        /// </summary>
        /// <param name="conversationName">Name of conversation. Must be unique.</param>
        /// <param name="type">Conversation type.</param>
        public ConversationAttribute(string conversationName, StateType type)
        {
            Name = conversationName;
            Type = type;
        }
    }
}