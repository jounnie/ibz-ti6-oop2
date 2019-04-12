using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

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
            // Calculate prime numbers using a simple (unoptimized) algorithm.
            IEnumerable<int> numbers = Enumerable.Range(3, 1000);

            var parallelQuery =
            from n in numbers.AsParallel()
            where Enumerable.Range (2, (int) Math.Sqrt (n)).All (i => n % i > 0)
            select n;
            int[] primes = parallelQuery.ToArray();

            foreach(int prime in primes)
            {
                Console.WriteLine("Primzahl :"+prime.ToString());

            }

            Thread.Sleep(10000);

        }
    }
}
