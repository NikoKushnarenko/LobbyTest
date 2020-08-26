using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using GrainInterfaces;
using Orleans;

namespace Client
{
    public class OrleansObserver : IReceiveMessage
    {
        private ISubscriptionManager _subscriptionManager;
        private IReceiveMessage _receiveMessageReference;
        private readonly IClusterClient _clusterClient;

        public OrleansObserver(IClusterClient clusterClient)
        {
            _clusterClient = clusterClient;
        }

        public void ReceiveMessage(string message) => Console.WriteLine(message);
        public async Task Init()
        {
            _subscriptionManager = _clusterClient.GetGrain<ISubscriptionManager>(0);
            _receiveMessageReference = await _clusterClient.CreateObjectReference<IReceiveMessage>(this);
            await _subscriptionManager.Subscribe(_receiveMessageReference);
        }

        public void Send(string message) => _subscriptionManager.Publish(message);

    }
}
