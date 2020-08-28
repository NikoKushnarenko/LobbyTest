using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace GrainInterfaces
{
    public interface IRoomGrain : IGrainWithGuidKey
    {
        Task Connect(IReceiveMessage observer);
        Task Exist(IReceiveMessage observer);
        Task PlayAction(string eventMessage);
    }
}
