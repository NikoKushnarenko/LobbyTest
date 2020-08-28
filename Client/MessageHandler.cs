using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using GrainInterfaces;
using Orleans;

namespace Client
{
    public class MessageHandler : IReceiveMessage
    {
        private IRoomGrain _roomGrain;
        private IReceiveMessage _receiveMessageReference;
        private readonly IClusterClient _clusterClient;
        private readonly Guid _id;

        public MessageHandler(IClusterClient clusterClient, Guid roomId)
        {
            _clusterClient = clusterClient;
            _id = roomId;
        }

        public Task ReceiveMessage(string message)
        {
            Console.WriteLine(message);
            return Task.CompletedTask;
        }

        public async Task Init()
        {
            _roomGrain = _clusterClient.GetGrain<IRoomGrain>(_id);
            _receiveMessageReference = await _clusterClient.CreateObjectReference<IReceiveMessage>(this);
            await _roomGrain.Connect(_receiveMessageReference);
        }

        public void Send(string message) => _roomGrain.PlayAction(message);
    }
}
