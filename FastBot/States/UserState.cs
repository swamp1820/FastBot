using FastBot.Enums;

namespace FastBot.States
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

        public ClientType Client { get; set; }

        public string ConversationState { get; set; }

        /// <summary>
        /// Bot awaiting for user answer.
        /// </summary>
        public bool MustAnswer { get; set; }

        /// <summary>
        /// Is state was changed in last conversation.
        /// </summary>
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