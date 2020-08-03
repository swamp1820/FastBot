using FastBot.Telegram.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types;

namespace FastBot.Telegram.Interfaces
{
    public interface IConversation<T> where T : UserState
    {
        Enum ConversationState { get; }

        T Execute(Message message, T userState);
    }
}
