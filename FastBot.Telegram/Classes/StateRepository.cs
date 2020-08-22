namespace FastBot.Telegram.Classes
{
    internal class StateRepository : BaseRepository<UserState>
    {
        public StateRepository() : base("UserStates")
        {
        }
    }
}