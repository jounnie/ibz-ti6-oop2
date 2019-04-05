using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Concurrent
{
    static class Program
    {
        private static readonly object balanceLock = new object();
        static int x = 0;
        static int y = 0;
        static int z = 0;
        static Await await = new Await();
        private static Mutex mut = new Mutex();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            for (int i = 1; i <= 50; i++)
            {
                x = y = z = 0;

                Thread s1 = new Thread(new ParameterizedThreadStart(
                    //Thread method
                    delegate
                    {
                        await.await(x > 0 && y > 0);
                        mut.WaitOne();
                        Thread.Sleep(500);
                        z = x + y;
                        mut.ReleaseMutex();
                    }));


                Thread s2 = new Thread(new ParameterizedThreadStart(
                    //Thread method
                    delegate
                    {
                        Thread.Sleep(500);
                        mut.WaitOne();
                        Thread.Sleep(500);
                        x = 1;
                        Thread.Sleep(500);
                        y = 3;
                        mut.ReleaseMutex();
                        @await.notify(true);
                    }));


                s2.Start();
                s1.Start();

                try
                {
                    s1.Join();
                    s2.Join();
                    Console.WriteLine(i + ". z = " + x + "+" + y + " = " + z);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }
        }
    }
}