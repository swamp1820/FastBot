using FastBot.Core;
using Microsoft.Extensions.Configuration;
using System;

namespace FastBot.Telegram.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json", true, true)
              .Build();

            // build bot
            var bot = new BotBuilder<User>()
                .UseState()
                .Build();

            // add clients and start bot
            bot
                .AddTelegram(configuration["TelegramToken"])
                .Start();

            Console.ReadLine();

            // stop
            bot.Stop();
        }
    }
}
