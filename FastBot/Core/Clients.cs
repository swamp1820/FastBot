using FastBot.Adapters;
using FastBot.Enums;
using FastBot.Messages;
using FastBot.States;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastBot.Core
{
    public class Clients
    {
        internal Dictionary<ClientType, IAdapter> Adapters = new Dictionary<ClientType, IAdapter>();

        /// <summary>
        /// Send message.
        /// </summary>
        public Task Send(Message message) => GetAdapter(message.ClientType).SendTextMessageAsync(message.ChatId, message.Text);

        internal void StopReceiving()
        {
            foreach (var adapter in Adapters.Values)
            {
                adapter.Stop();
            }
        }

        internal void StartReceiving()
        {
            foreach (var adapter in Adapters.Values)
            {
                adapter.Start();
            }
        }

        /// <summary>
        /// Send message.
        /// </summary>
        public Task Send(UserState user, string message) => GetAdapter(user.Client).SendTextMessageAsync(user.Id, message);

        /// <summary>
        /// Send message.
        /// </summary>
        public Task Send(UserState user, string message, Keyboard keyboard) => GetAdapter(user.Client).SendTextMessageAsync(user.Id, message, keyboard);

        private IAdapter GetAdapter(ClientType clientType) => Adapters.Where(x => x.Key == clientType).Single().Value;
    }
}