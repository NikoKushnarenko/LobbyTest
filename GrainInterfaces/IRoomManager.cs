using Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace GrainInterfaces
{
    public interface IRoomManager : IGrainWithIntegerKey, IGrainWithGuidKey
    {
        Task<RoomInfoDTO[]> GetAllRoom();
    }
}
