using System;
using System.Collections.Generic;
using System.Text;

namespace FastBot.Telegram.Classes
{
    public class StateRepository : BaseRepository<UserState>
    {
        public StateRepository() : base("UserStates")
        {
        }
    }
}
