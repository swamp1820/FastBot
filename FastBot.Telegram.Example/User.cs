using FastBot.Telegram.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastBot.Telegram.Example
{
    public class User : UserState
    {
        public User() : base()
        {
            ConversationState = Conv.Hello;
        }

        public string SomeInfo { get; set; }
    }
}
