# FastBot.Telegram
Library for telegram bot rapid prototyping.   
The package makes it easy to build complex conversations without thinking about infrastructure out of the box.

## Features
- User state management
- Easy conversation control

## Quick start
1) Install package    
`dotnet add PROJECT package FastBot.Telegram --version 0.1.0`
2) Create model for user state by inherit [UserState](FastBot.Telegram/Classes/UserState.cs).
``` c#
public class User : UserState
{
    public string SomeInfo { get; set; }
}
```
3) Initialize bot
``` c#
static void Main(string[] args)
{
    Bot<User>.Init("bot_token");
    Console.ReadLine();
}
```
4) Create conversations by inherit [BaseConversation<T>](FastBot.Telegram/Classes/BaseConversation.cs).   
[Examples](FastBot.Telegram.Example/Conversations)
> Mark your starting conversation class with `StateType.Start` attribute    
> Switch conversation by `userState.SetConversationState("next_conversation_name")`   
