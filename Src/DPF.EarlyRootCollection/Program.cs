using System;
using System.Threading;

namespace DPF.EarlyRootCollection
{
    class Program
    {
        /// <summary>
        /// Test cases:
        /// Uncomment samples separately and run in: Debug, Release, Release with enabled TieredCompilation in csproj
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var timer = new Timer(x => Console.WriteLine(DateTime.UtcNow), null, 0, 100);
            Console.WriteLine("Timer started?");
            GC.Collect();
            Console.Read();

            //    var timer = new Timer(x => Console.WriteLine(DateTime.UtcNow), null, 0, 100);
            //    Console.WriteLine("Timer started?");
            //    GC.Collect();
            //    Console.Read();
            //    GC.KeepAlive(timer);

            //using var timer = new Timer(x => Console.WriteLine(DateTime.UtcNow), null, 0, 100);
            //Console.WriteLine("Timer started?");
            //GC.Collect();
            //Console.Read();
        }
    }
}
