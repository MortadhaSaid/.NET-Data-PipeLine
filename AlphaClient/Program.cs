using NamedPipeLib;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AlphaClient
{
    class Program
    {
        private static string pipeName = "Demo2Pipe";
        static void Main(string[] args)
        {
            Thread.Sleep(5000);
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
           
            Console.ReadLine();
        }
        static void Client(string clientName)
        {
            using (var client = new NamedPipeClient(pipeName))
            {
                var request = clientName + " Request a";
                var response = client.SendRequest(request);
                Console.WriteLine(response);
                Task.Delay(1000).Wait();

               /* var request1 = clientName + " Request b";
                var response1 = client.SendRequest(request1);
                Console.WriteLine(response1);
                Task.Delay(100).Wait();

                var request2 = clientName + " Request c";
                var response2 = client.SendRequest(request2);
                Console.WriteLine(response2);*/
            }
        }
    }

}


