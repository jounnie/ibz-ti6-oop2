using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

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
            File.Delete("lp.html");
            File.Delete("jaoo.htm");
            stopWatch.Start();
            WebClient client = new WebClient();
            client.DownloadFile("http://www.linqpad.net", "lp.html");
            WebClient client2 = new WebClient();
            client2.DownloadFile("http://www.jaoo.dk", "jaoo.htm");
            stopWatch.Stop();
            Console.WriteLine("Sequence Time [ms] : " + stopWatch.ElapsedMilliseconds);

            File.Delete("lp.html");
            File.Delete("jaoo.htm");
            stopWatch.Reset();
            stopWatch.Start();
            Parallel.Invoke(
             () => new WebClient().DownloadFile("http://www.linqpad.net", "lp.html"),
             () => new WebClient().DownloadFile("http://www.jaoo.dk", "jaoo.html"));
            stopWatch.Stop();
            Console.WriteLine("Paralle Time [ms] : " + stopWatch.ElapsedMilliseconds);

            Thread.Sleep(10000);

        }
    }
}
