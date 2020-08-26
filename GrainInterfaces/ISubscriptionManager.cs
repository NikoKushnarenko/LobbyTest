using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace GrainInterfaces
{
    public interface ISubscriptionManager : IGrainWithIntegerKey
    {
        Task Subscribe(IReceiveMessage observer);
        Task Unsubscribe(IReceiveMessage observer);
        Task Publish(string eventMessage);
    }
}
