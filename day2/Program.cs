using System;
using System.Threading.Tasks;

namespace day2
{
    class Program
    {
        static void Main(string[] args)
        {
            //ex2112_8();
            ex2112_10();
        }

        private static void ex2112_10()
        {
            long zahl = Convert.ToInt32(13); // Kommandozeilenparameter
            long wurzel = (long)Math.Sqrt(zahl);
            long teiler = 2;
            
            /*for (teiler = 2; teiler <= wurzel; teiler++)
            {
            if ((zahl % teiler) == 0)
            break;
            }*/

            var p1 = Parallel.For(teiler, wurzel, (i, loopState) =>
            {
                if ((zahl % i) == 0)
                    loopState.Break();
            });

            if (p1.IsCompleted)
            {
                Console.WriteLine(zahl+" ist eine Primzahl.");
            }
            else
            {
                Console.WriteLine(zahl+" ist keine Primzahl.");
            }
        }

        private static void ex2112_8()
        {
            int n = Convert.ToInt32(Console.ReadLine()); // Kommandozeilenparameter
            long result = 1;
            //for (int i = 1; i <= n; i++)
            //result *= i;
            Parallel.For(1, n + 1, i => result *= i);
            Console.WriteLine(n + "! = " + result);
        }
    }
}