using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NamedPipeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Run Demo 1:");
            Demo1.Run();
            Console.WriteLine();
            Console.WriteLine("Run Demo 2:");
            Demo2.Run();
            Console.ReadLine();
        }
    }
}
