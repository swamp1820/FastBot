# FastBot
Library for omnichanel bot rapid prototyping.   
The package makes it easy to build complex conversations out of the box, great for single/omnichannel chatbot low-to-medium complexity or webhook receiver.


## Features
- Support multiple channels simultaneously (customizable)
- Fast setup (only credentials needed)
- Local serverless DB id default configuration (customizable)


## Supported channels
- Telegram
- Vk
### Soon
- HTTP API
- Discord


## Quick start
1) Install package    
`dotnet add PROJECT package FastBot`
2) Create model for user state by inherit [UserState](FastBot/States/UserState.cs).
``` c#
public class User : UserState
{
    public string SomeInfo { get; set; }
}
```
3) Setup and start bot
``` c#
static void Main(string[] args)
{
            // build bot
            var bot = new BotBuilder<User>()
                .UseState()
                .Build();

            // add clients and start bot
            bot
                .AddTelegram("TelegramToken")
                .Start();

            Console.ReadLine();

            // stop
            bot.Stop();
}
```
4) Create conversations by inherit [BaseConversation<T>](FastBot/Conversations/BaseConversation.cs).   
[Examples](/examples/FastBot.Example.Echo/Conversations)
> Mark your starting conversation class with `StateType.Start` attribute    
> Switch conversation by `userState.SetConversationState("next_conversation_name")`   
