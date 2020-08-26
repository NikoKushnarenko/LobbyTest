using System;
using System.Threading.Tasks;
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
                var cline = new OrleansObserver(client);
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

    }
}
