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

            var bot = new BotBuilder<User>()
                .AddTelegram(configuration["TelegramToken"])
                .AddState()
                .Build();

            bot.Start();
            Console.ReadLine();
            bot.Stop();
        }
    }
}
