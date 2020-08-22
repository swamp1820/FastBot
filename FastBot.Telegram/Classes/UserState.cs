namespace FastBot.Telegram.Classes
{
    public abstract class UserState
    {
        public UserState()
        {
        }

        protected UserState(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

        public string ConversationState { get; set; }

        public bool MustAnswer { get; set; }

        public bool StateChanged { get; set; }

        /// <summary>
        /// Sets conversation state.
        /// </summary>
        /// <param name="stateName">State name.</param>
        public void SetConversationState(string stateName)
        {
            MustAnswer = false;
            StateChanged = true;
            ConversationState = stateName;
        }
    }
}