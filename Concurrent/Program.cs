using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Concurrent
{
    static class Program
    {
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

                Task s1 = Task.Run(() =>
                {
                    await.await(x > 0 && y > 0);
                    mut.WaitOne();
                    Task.Delay(500);
                    z = x + y;
                    mut.ReleaseMutex();
                });


                Task s2 = Task.Run(() =>
                {
                    Task.Delay(500);
                    mut.WaitOne();
                    Task.Delay(500);
                    x = 1;
                    Task.Delay(500);
                    y = 3;
                    mut.ReleaseMutex();
                });
                s2.GetAwaiter().OnCompleted(() => @await.notify(true));

                try
                {
                    s1.Wait();
                    s2.Wait();
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