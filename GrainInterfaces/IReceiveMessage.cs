using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace GrainInterfaces
{
    public interface IReceiveMessage : IGrainObserver
    {
        void ReceiveMessage(string message);
    }
}
