using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Model;
using GrainInterfaces;
using Orleans;
using Orleans.Configuration;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var client = await ConnectClient())
            {
                var existRoom = await GetRoomsAsync(client);
                Console.WriteLine("Enter a number room");
                var number = int.Parse(Console.ReadLine());
                var cline = new MessageHandler(client, existRoom[number].IdRoom);
                await cline.Init();
                while (true)
                {
                    Console.WriteLine("Send massage: ");
                    var message = Console.ReadLine();
                    cline.Send(message);
                    Console.ReadKey();
                }
            }
        }
        private static async Task<IClusterClient> ConnectClient()
        {
            IClusterClient client;
            client = new ClientBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "OrleansBasics";
                })
                .Build();

            await client.Connect();
            return client;
        }

        public static async Task<RoomInfoDTO[]> GetRoomsAsync(IClusterClient clusterClient)
        {
             
             IRoomManager _iRoomManager;
             Guid _id;
             _iRoomManager = clusterClient.GetGrain<IRoomManager>(Guid.Empty);
             var rooms =  await _iRoomManager.GetAllRoom();
             return rooms;
        }
    }
}
