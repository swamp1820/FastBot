using FastBot.Core;
using FastBot.States;
using System;
using Microsoft.Extensions.Configuration;

namespace FastBot.Example.Global
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json", true, true)
              .Build();

            // build bot
            var bot = new BotBuilder<User>()
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
