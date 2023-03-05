using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NamedPipeLib
{
    public class NamedPipeServer
    {
        static Mutex MTX = new Mutex();
        private readonly string pipeName;
        private readonly int maxNumberOfServerInstances;

        private List<NamedPipeServerInstance> servers = new List<NamedPipeServerInstance>();

        public event EventHandler<PipeMsgEventArgs> newRequestEvent = delegate { };

        public NamedPipeServer(string pipeName) : this(pipeName, 30, 12) { }

        public NamedPipeServer(string pipeName, int maxNumberOfServerInstances, int initialNumberOfServerInstances)
        {
            this.pipeName = pipeName;
            this.maxNumberOfServerInstances = maxNumberOfServerInstances;

            for (int i = 0; i < initialNumberOfServerInstances; i++)
            {
                NewServerInstance();
                Console.WriteLine(i);
            }
        }

        public void Dispose()
        {
            CleanServers(true);
        }

        private void NewServerInstance()
        {
            // Start a new server instance only when the number of server instances
            // is smaller than maxNumberOfServerInstances
            if (servers.Count < maxNumberOfServerInstances)
            {
                if (servers.Contains(null))
                    Console.WriteLine("YES THERES IS NYLL");
                Console.WriteLine(" i am creating a new instance : ( old instance) "+servers.Count());
                var server = new NamedPipeServerInstance(pipeName, maxNumberOfServerInstances);

                server.newServerInstanceEvent += (s, e) => NewServerInstance();

                server.newRequestEvent += (s, e) => newRequestEvent.Invoke(s, e);

                servers.Add(server);

            }
            MTX.WaitOne();
            // Run clean servers anyway
            CleanServers(false);
            MTX.ReleaseMutex();
        }

        /// <summary>
        /// A routine to clean NamedPipeServerInstances. When disposeAll is true,
        /// it will dispose all server instances. Otherwise, it will only dispose
        /// the instances that are completed, canceled, or faulted.
        /// PS: disposeAll is true only for this.Dispose()
        /// </summary>
        /// <param name="disposeAll"></param>
        private void CleanServers(bool disposeAll)    
        {
           

            if (disposeAll)
            {
                foreach (var server in servers)
                {
                    server.Dispose();
                }
            }
            else
            {
                for (int i = servers.Count - 1; i >= 0; i--)
                {
                    if (i >= 0)
                    {
                        if (servers[i] == null)
                        {
                            servers.RemoveAt(i);
                        }
                        else if (servers[i].TaskCommunication != null &&
                            (servers[i].TaskCommunication.Status == TaskStatus.RanToCompletion ||
                            servers[i].TaskCommunication.Status == TaskStatus.Canceled ||
                            servers[i].TaskCommunication.Status == TaskStatus.Faulted))
                        {
                            servers[i].Dispose();
                            Console.WriteLine("The I is now : " + i +" and count is "+servers.Count());
                            servers.RemoveAt(i);

                        }
                    }
                
               /* foreach ( var i in servers)    
               
                    {
                        
                        
                            if (i == null)
                            {
                        servers.Remove(i);
                            }
                            else if (i.TaskCommunication != null &&
                                (i.TaskCommunication.Status == TaskStatus.RanToCompletion ||
                                i.TaskCommunication.Status == TaskStatus.Canceled ||
                                i.TaskCommunication.Status == TaskStatus.Faulted))
                            {
                                i.Dispose();
                                Console.WriteLine("The I is now : " + i + " and count is " + servers.Count());
                                servers.Remove(i);

                            }*/
                        

                    }
              
            }
           
        }
    }
}
