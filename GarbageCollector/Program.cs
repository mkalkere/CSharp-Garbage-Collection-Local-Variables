using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace GarbageCollector
{
    class Program
    {
        static void Main(string[] args)
        {

            var cts = new CancellationTokenSource(3000);
            try
            {
                Test(cts.Token);
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }

            Console.ReadLine();
        }

        static void Test(CancellationToken token)
        {
            Callback callBack = new Callback();
            //GC.KeepAlive(callBack);

            while (true)
            {
                token.ThrowIfCancellationRequested();

                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
                GC.WaitForPendingFinalizers();

                Thread.Sleep(1000);
            }

            
        }
    }

    public class Callback
    {
        public Callback() { }

        ~Callback() { WriteLine("~Callback"); }
    }


}
