namespace FastBot.States
{
    internal class StateRepository : BaseRepository<UserState>
    {
        public StateRepository() : base("UserStates")
        {
        }
    }
}