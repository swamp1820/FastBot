using FastBot.Telegram.Classes;

namespace FastBot.Telegram.Example
{
    public class User : UserState
    {
        public User() : base()
        {
        }

        public string SomeInfo { get; set; }
    }
}