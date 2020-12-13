using FastBot.Core;
using FastBot.Messages;
using FastBot.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VkNet;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.Keyboard;
using VkNet.Model.RequestParams;

namespace FastBot.Adapters
{
    internal class VkAdapter<T> : BaseAdapter<T>, IAdapter where T : UserState, new()
    {
        internal VkApi client;
        private Thread workerThread;
        private ulong groupId;

        public VkAdapter(Engine<T> engine, VkApi client, ulong groupId) : base(engine)
        {
            this.client = client;
            this.groupId = groupId;
        }
        public Task SendTextMessageAsync(long id, string message)
        {
            Random rnd = new Random();
            return client.Messages.SendAsync(new MessagesSendParams()
            {
                UserId = id,
                Message = message,
                RandomId = rnd.Next(),
            });
        }

        public Task SendTextMessageAsync(long id, string message, Keyboard keyboard)
        {
            Random rnd = new Random();
            var bt = new List<List<MessageKeyboardButton>>();
            foreach (var row in keyboard.Buttons.ToList())
            {
                var r = new List<MessageKeyboardButton>();
                foreach (var button in row.ToList())
                {
                    r.Add(new MessageKeyboardButton()
                    {
                        
                        Action = new MessageKeyboardButtonAction()
                        {
                            Type = KeyboardButtonActionType.Text,
                            Label = button.Text
                        }
                    });
                }

                bt.Add(r);
            }
            var kb = new MessageKeyboard()
            {
                OneTime = keyboard.OneTime,
                Inline = false,
                Buttons = bt
            };

            return client.Messages.SendAsync(new MessagesSendParams()
            {
                UserId = id,
                Message = message,
                Keyboard = kb,
                RandomId = rnd.Next(),
            });
        }

        public void Start()
        {
            workerThread = new Thread(async () =>
            {
                var s = client.Groups.GetLongPollServer(groupId);
                while (true)
                {
                    var poll = await client.Groups.GetBotsLongPollHistoryAsync(
                            new BotsLongPollHistoryParams()
                            { Server = s.Server, Ts = s.Ts, Key = s.Key, Wait = 25 });
                    s.Ts = poll?.Ts;
                    if (poll?.Updates == null) continue;
                    foreach (var a in poll.Updates)
                    {
                        if (a.Type == GroupUpdateType.MessageNew)
                        {
                            var message = new Message()
                            {
                                ChatId = (long)a.Message.FromId,
                                ClientType = Enums.ClientType.Vk,
                                Text = a.Message.Text
                            };

                            Engine.MessageReceivedAsync(message);
                        }
                    }

                }
            });
            workerThread.Start();
        }

        public void Stop()
        {
            workerThread.Abort();
        }
    }
}
