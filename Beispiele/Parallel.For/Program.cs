using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace Concurrent
{
    static class Program
    {
       
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Stopwatch stopWatch = new Stopwatch();
            var keyPairs = new string[6];
            stopWatch.Start();
            for (int i = 0; i < keyPairs.Length; i++)
            {
              keyPairs[i] = RSA.Create().ToXmlString(true);
            }
            stopWatch.Stop();
            Console.WriteLine("Sequence Time [ms] : " + stopWatch.ElapsedMilliseconds);

            keyPairs = new string[6];
            stopWatch.Reset();
            stopWatch.Start();
            Parallel.For(0, keyPairs.Length,
            i => keyPairs[i] = RSA.Create().ToXmlString(true));
            stopWatch.Stop();
            Console.WriteLine("Paralle Time [ms] : " + stopWatch.ElapsedMilliseconds);

            Thread.Sleep(10000);

        }
    }
}
