using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NamedPipeLib;

namespace NamedPipeDemo
{
    class Demo2
    {
        private static string pipeName = "Demo2Pipe";

        public static void Run()
        {
            Task.Run(() => Server());

            Task.Delay(300).Wait();

            var clients = new List<string>()
            {
                "Client 1",
                "Client 2",
                "Client 3",
                "Client 4",
                "Client 5",
                "Client 6",
                "Client 7",
                "Client 8",
                "Client 9",
                "Client 10",
                "Client 11",
                "Client 12",
                "Client 13",
                "Client 14",
                "Client 15",
                "Client 16",
                "Client 17",
                "Client 18",
                "Client 19",
                "Client 20",
                "Client 21",
                "Client 22",
                "Client 23",
                "Client 24"
            };

            Parallel.ForEach(clients, (c) => Client(c));
        }

        static void Server()
        {
            var server = new NamedPipeServer(pipeName);
            server.newRequestEvent += (s, e) => e.Response = "Echo. " + e.Request;

            Task.Delay(500).Wait();
            server.Dispose();
        }

        static void Client(string clientName)
        {
            using (var client = new NamedPipeClient(pipeName))
            {
                var request = clientName + " Request a";
                var response = client.SendRequest(request);
                Console.WriteLine(response);
                Task.Delay(100).Wait();

                var request1 = clientName + " Request b";
                var response1 = client.SendRequest(request1);
                Console.WriteLine(response1);
                Task.Delay(100).Wait();

                var request2 = clientName + " Request c";
                var response2 = client.SendRequest(request2);
                Console.WriteLine(response2);
            }
        }
    }
}
