using FastBot.Conversations;
using FastBot.Core;
using FastBot.Enums;
using FastBot.Example.Repo;
using FastBot.Messages;
using FastBot.States;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json", true, true)
              .Build();

// build bot
var bot = new BotBuilder<User>()
    .UseState(typeof(CustomRepository))
    .Build();

// add clients and start bot
bot
    .AddTelegram(configuration["TelegramToken"])
    .Start();


Console.ReadLine();
// stop
bot.Stop();

public class User : UserState
{
    public User() { }
    public User(long id) : base(id)
    {

    }
    internal string PrevText { get; set; }
}

[Conversation(nameof(Echo), StateType.Start)]
internal class Echo : BaseConversation<User>
{
    public Echo(Clients clients) : base(clients)
    {

    }

    public override async Task AskQuestion(User userState)
    {
        await Clients.Send(userState, "I'll repeat previous text");
    }

    public override async Task CheckAnswer(Message message, User userState)
    {
        await Clients.Send(userState, $"Previous text was:{userState.PrevText}");
        userState.PrevText = message.Text;
    }
}
