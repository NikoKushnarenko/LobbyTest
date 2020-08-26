using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GrainInterfaces;
using Orleans;

namespace Grains
{
    public class SubscriptionManagerGrain : Grain, ISubscriptionManager
    {
        private ObserverSubscriptionManager<IReceiveMessage> _subscriptionManager;

        public SubscriptionManagerGrain()
        {
        }

        public override async Task OnActivateAsync()
        {
            _subscriptionManager = new ObserverSubscriptionManager<IReceiveMessage>();
            _subscriptionManager.ExpirationDuration = TimeSpan.FromMinutes(60);

            await base.OnActivateAsync();
        }
        public Task Publish(string eventMessage)
        {
            _subscriptionManager.Notify(s => s.ReceiveMessage(eventMessage));
            return Task.CompletedTask;
        }

        public Task Subscribe(IReceiveMessage observer)
        {
            _subscriptionManager.Subscribe(observer);

            return Task.CompletedTask;
        }

        public Task Unsubscribe(IReceiveMessage observer)
        {
            if (_subscriptionManager.IsSubscribed(observer))
            {
                _subscriptionManager.Unsubscribe(observer);
            }
            return Task.CompletedTask;
        }

    }
}
