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

        public Task Send(Message message)
        {
            return Adapters.Where(x => x.Key == message.ClientType).Single().Value.SendTextMessageAsync(message.ChatId, message.Text);
        }

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

        public Task Send(UserState user, string message)
        {
            return Adapters.Where(x => x.Key == user.Client).Single().Value.SendTextMessageAsync(user.Id, message);
        }

        public Task Send(UserState user, string message, Keyboard keyboard)
        {
            return Adapters.Where(x => x.Key == user.Client).Single().Value.SendTextMessageAsync(user.Id, message, keyboard);
        }
    }
}