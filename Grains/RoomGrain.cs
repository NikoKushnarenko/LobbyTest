using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Enum;
using Common.Extension;
using GrainInterfaces;
using Orleans;

namespace Grains
{
    public class RoomGrain : Grain, IRoomGrain
    {
        public Guid Id
        {
            get => Id;
            set
            {
                if (Id == default(Guid))
                {
                    Id = value;
                }
            }
        }

        private ObserverSubscriptionManager<IReceiveMessage> _subscriptionManager;

        public override async Task OnActivateAsync()
        {
            _subscriptionManager = new ObserverSubscriptionManager<IReceiveMessage>();
            _subscriptionManager.ExpirationDuration = TimeSpan.FromMinutes(60);

            await base.OnActivateAsync();
        }
        public Task PlayAction(string eventMessage)
        {
            _ = _subscriptionManager.Notify(s => s.ReceiveMessage(eventMessage));
            return Task.CompletedTask;
        }

        public Task Connect(IReceiveMessage observer)
        {
            _subscriptionManager.Subscribe(observer);
            Notification(observer, SystemMessage.Greeting);
            return Task.CompletedTask;
        }

        public Task Exist(IReceiveMessage observer)
        {
            if (_subscriptionManager.IsSubscribed(observer))
            {
                _subscriptionManager.Unsubscribe(observer);
                Notification(observer, SystemMessage.Farewell);
            }
            return Task.CompletedTask;
        }

        private Task Notification(IReceiveMessage observer, SystemMessage message)
        {
            _ =_subscriptionManager.Notify(s => s.ReceiveMessage($"{observer} {message.ToDescription()}"));
            return Task.CompletedTask;
        } 

    }
}
