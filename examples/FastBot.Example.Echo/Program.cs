using FastBot.Core;
using System;
using Microsoft.Extensions.Configuration;

namespace FastBot.Example.Echo
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
                .UseState()
                .Build();

            // add clients and start bot
            bot
                .AddTelegram(configuration["TelegramToken"])
                //.AddVk(configuration["VkToken"], Convert.ToUInt64(configuration["VkGroupId"]))
                .Start();


            Console.ReadLine();
            // stop
            bot.Stop();
        }
    }
}
