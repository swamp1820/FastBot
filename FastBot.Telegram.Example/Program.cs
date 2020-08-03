using System;

namespace FastBot.Telegram.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Bot<User>.Init("bot_token");
            Console.ReadLine();
        }
    }
}
