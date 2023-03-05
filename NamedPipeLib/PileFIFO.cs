using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NamedPipeLib
{
    class PileFIFO
    {
        public void FileMutexer(string Data, Mutex RTX, int hint)
        {
            string pathe = System.IO.Path.GetDirectoryName(
        System.Reflection.Assembly.GetExecutingAssembly().Location);
            string filex = pathe + @"\Data.txt";

            RTX.WaitOne();
            if (hint == 0)
            {
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
               
            }
            else if(hint==1)
            {

            }
            RTX.ReleaseMutex();
        }
    }
}
