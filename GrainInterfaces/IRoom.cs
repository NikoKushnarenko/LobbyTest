using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace GrainInterfaces
{
    public interface IRoom : IGrainWithIntegerKey
    {
        Task ReceiveMessage(string message);
    }
}
