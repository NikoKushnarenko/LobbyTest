using System;
using System.Collections.Concurrent;
using Orleans;
using GrainInterfaces;
using System.Threading.Tasks;
using Orleans.Runtime;

namespace Grains
{
    public class Room : Grain
    {
        private ConcurrentDictionary<Guid, string> guids;
        public Task<string> Close(Guid roomId)
        {
            var guid = Guid.NewGuid();
            guids.TryRemove(guid, out var value);
            return Task.FromResult(value);
        }

        public Task<Guid> Create()
        {
            var guid = Guid.NewGuid();
            guids.TryAdd(guid, "TEST");
            return Task.FromResult(guid);
        }

        public Task Enter(Guid roomId)
        {
            throw new NotImplementedException();
        }

        public Task<string> Kick(string user)
        {
            throw new NotImplementedException();
        }
    }
}
