using System;
using System.Threading;
using System.Threading.Tasks;

namespace DPF.EarlyRootCollection
{
    class Program
    {
        /// <summary>
        /// Test cases:
        /// Uncomment samples separately and run in: Debug, Release, Release with enabled TieredCompilation in csproj
        /// </summary>
        /// <param name="args"></param>
        static async Task Main(string[] args)
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

            //var timer = new Timer(x => Console.WriteLine(DateTime.UtcNow), null, 0, 100);
            //Console.WriteLine("Timer started?");
            //GC.Collect();
            //Console.Read();
            //timer.Dispose();

            //using (var timer = new Timer(x => Console.WriteLine(DateTime.UtcNow), null, 0, 100))
            //{
            //    GC.Collect();
            //    GC.Collect();
            //    Console.Read();
            //}

            //using (var timer = new Timer(x => Console.WriteLine(DateTime.UtcNow), null, 0, 100))
            //{
            //    GC.Collect();
            //    GC.Collect();
            //    Console.Read();
            //}

            //await using (var timer = new Timer(x => Console.WriteLine(DateTime.UtcNow), null, 0, 100))
            //{
            //    GC.Collect();
            //    GC.Collect();
            //    Console.Read();
            //}
        }
    }
}
