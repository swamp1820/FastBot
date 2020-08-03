using System;
using System.Collections.Generic;
using System.Text;

namespace FastBot.Telegram.Classes
{
    public abstract class UserState
    {
        public UserState()
        {
        }

        protected UserState(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

        public Enum ConversationState { get; set; }
    }
}
