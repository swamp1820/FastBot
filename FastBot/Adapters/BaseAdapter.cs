using FastBot.Core;
using FastBot.States;

namespace FastBot.Adapters
{
    internal abstract class BaseAdapter<T> where T : UserState, new()
    {
        protected readonly Engine<T> Engine;

        protected BaseAdapter(Engine<T> engine)
        {
            Engine = engine;
        }
    }
}