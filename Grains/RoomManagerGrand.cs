using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using GrainInterfaces;
using Orleans;

namespace Grains
{
    public class RoomManagerGrand : Grain, IRoomManager
    {
        public ConcurrentDictionary<Guid, IRoomGrain> Rooms;
        public override Task OnActivateAsync()
        {
            //Test init rooms
            Rooms = new ConcurrentDictionary<Guid, IRoomGrain>();
            InitNewRooms();
            return base.OnActivateAsync();
        }

        public Task<RoomInfoDTO[]> GetAllRoom()
        {
            List<RoomInfoDTO> listRooms = new List<RoomInfoDTO>();
            foreach (var room in Rooms)
            {
                listRooms.Add(new RoomInfoDTO()
                {
                    Name = "Test",
                    IdRoom = room.Key
                });
            }

            return Task.FromResult(listRooms.ToArray());
        }

        private void InitNewRooms()
        {
            var FirstId = Guid.NewGuid();
            var roomOne = this.GrainFactory.GetGrain<IRoomGrain>(FirstId);
            Rooms.TryAdd(FirstId, roomOne);

            var SecondId = Guid.NewGuid();
            var roomSecond = this.GrainFactory.GetGrain<IRoomGrain>(FirstId);
            Rooms.TryAdd(SecondId, roomSecond);
        }
    }

}
