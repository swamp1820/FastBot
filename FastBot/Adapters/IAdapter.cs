using FastBot.Messages;
using System.Threading.Tasks;

namespace FastBot.Adapters
{
    public interface IAdapter
    {
        void Start();

        void Stop();

        Task SendTextMessageAsync(long id, string message);

        Task SendTextMessageAsync(long id, string message, Keyboard keyboard);
    }
}