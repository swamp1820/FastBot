using System.Collections.Generic;

namespace FastBot.Messages
{
    public class Keyboard
    {
        public IEnumerable<IEnumerable<Button>> Buttons { get; set; }
        public bool Resize { get; set; }
        public bool OneTime { get; set; }
    }
}