using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using NamedPipeLib;

namespace NamedPipeDemo
{
    class Demo2
    {
        private static string pipeName = "Demo2Pipe";

        static void Main(string[] args)
        {         
            Server();
               
        }

        static void Server()
        {
            
            var server = new NamedPipeServer(pipeName);

            server.newRequestEvent += (s, e) => e.Response = "Echo. " + e.Request;
           while (true)
          {
               Task.Delay(20000).Wait();
                // server.Dispose();
           } 
        }
       /* static public void ThreadSender()
        {
            string macAddr =
             (
             from nic in NetworkInterface.GetAllNetworkInterfaces()
             where nic.OperationalStatus == OperationalStatus.Up
             select nic.GetPhysicalAddress().ToString()
             ).FirstOrDefault();
            string UserKey = macAddr;

            string xmlhead = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:tem=\"http://tempuri.org/\">" +
                "<soapenv:Header/>" +
                "<soapenv:Body>" +
                "<tem:InsertMany>" +
                "<!--Optional:-->" +
                "<tem:L1>" +
                "<!--Zero or more repetitions:-->";

            string xmlend = "</tem:L1>" +
                           "</tem:InsertMany>" +
                           "</soapenv:Body>" +
                           "</soapenv:Envelope>";


            while (true)
            {
                Thread.Sleep(60000);
                Console.WriteLine("This is the beginning after the sleep");
                Thread.Sleep(2000);
                string[] DATA = WorkingMutex(1, null, 0);
                List<Event> DataList = TransformingData(DATA);
                if (DataList.Count() > 0)
                {
                    int NUM = SendData(DataList, xmlhead, xmlend, UserKey);
                    if (NUM > 0)
                    {
                        Console.WriteLine("Request DOne");
                        var Y = WorkingMutex(3, null, NUM);
                        Console.WriteLine("Deletion is DOne");
                    }

                }
            }
        }*/


    }
}
