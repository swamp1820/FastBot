using FastBot.Enums;

namespace FastBot.Messages
{
    public class Message
    {
        public long ChatId { get; set; }
        public string Text { get; set; }
        public ClientType ClientType { get; set; }
    }
}