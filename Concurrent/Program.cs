using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Concurrent
{
    static class Program
    {
        static int x = 0;
        static int y = 0;
        static int z = 0;
        
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
                        Thread.Sleep(500);
                        z = x + y;
                    }));


                Thread s2 = new Thread(new ParameterizedThreadStart(
                    //Thread method
                    delegate
                    {
                        Thread.Sleep(500);
                        x = 1;
                    }));


                Thread s3 = new Thread(new ParameterizedThreadStart(
                    //Thread method
                    delegate
                    {
                        Thread.Sleep(500);
                        y = 2;
                    }));

                s2.Start();
                s1.Start();
                s3.Start();

                try
                {
                    s1.Join();
                    //s2.Join();
                    s3.Join();
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
