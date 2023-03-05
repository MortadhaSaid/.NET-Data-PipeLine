using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NamedPipeLib
{
    class NamedPipeServerInstance : IDisposable
    {
        static Mutex RTX = new Mutex();
        private NamedPipeServerStream server;
        private bool disposeFlag = false;

        public Task TaskCommunication { get; private set; }

        public event EventHandler newServerInstanceEvent = delegate { };
        public event EventHandler<PipeMsgEventArgs> newRequestEvent = delegate { };

        public NamedPipeServerInstance(string pipeName, int maxNumberOfServerInstances)
        {
            server = new NamedPipeServerStream(pipeName, PipeDirection.InOut, maxNumberOfServerInstances, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
            var asyncResult = server.BeginWaitForConnection(OnConnected, null);
        }

        public void Dispose()
        {
            disposeFlag = true;
            Console.WriteLine("this instance is about to be deleted");
            server.Dispose();
        }

        private void OnConnected(IAsyncResult result)
        {
            /// This method might be invoked either on new client connection
            /// or on dispose. Thus, it is necessary to check disposeFlag.
            if (!disposeFlag)
            {
                server.EndWaitForConnection(result);
                Console.WriteLine("i connected");
                newServerInstanceEvent.Invoke(this, EventArgs.Empty);                

                TaskCommunication = Task.Factory.StartNew(Communication);
               
            }
        }

        private void Communication()
        {
            using (var reader = new StreamReader(server))
            {
                while (!reader.EndOfStream)
                {
                    var request = reader.ReadLine();

                    if (request != null)
                    {
                        /* var msgEventArgs = new PipeMsgEventArgs(request);
                         newRequestEvent.Invoke(this, msgEventArgs);
                         var response = msgEventArgs.Response + Environment.NewLine;

                         var bytes = Encoding.UTF8.GetBytes(response)
                         server.Write(bytes, 0, bytes.Count());*/
                        Console.WriteLine(request.ToString());
                        var PL= new PileFIFO();
                        PL.FileMutexer(request.ToString(),RTX,0);
                       // FileMutexer(request.ToString());
                    }
                }
            }
            Task.Delay(1000).Wait();
            server.Dispose();
        }
        public void FileMutexer(string Data)
        {
            string pathe = System.IO.Path.GetDirectoryName(
        System.Reflection.Assembly.GetExecutingAssembly().Location);
            string filex = pathe + @"\Data.txt";
            RTX.WaitOne();
            if (File.Exists(filex))
            {
                TextWriter tsm = new StreamWriter(filex, true);
                tsm.WriteLine(Data);
                tsm.Close();
            }
            else
            {
                using (var streamWriter = File.AppendText(filex))
                {
                }
            }
            RTX.ReleaseMutex();
        }
    }
}
