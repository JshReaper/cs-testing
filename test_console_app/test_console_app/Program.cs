using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace test_console_app
{
    internal static class Program
    {
        private static bool _talking = true;
        private static void Main(string[] args)
        {
            Thread t = new Thread(WriteY);
            t.Start();
            Console.ReadKey();
            for (int i = 0; i < 1000; i++)
            {
                Console.Write("x");
            }
            Console.ReadKey();
            
        }

        private static void WriteY()
        {
            Console.ReadKey();
            for (int i = 0; i < 1000; i++)
            {
                Console.Write("y");
            }
        }


        private static void EnCounterDog()
        {
            var fido = new GoldenRetriever();
            Console.WriteLine("ur dog just come running in say Hi");
            var userInput = Console.ReadLine();

            if (userInput == "Hi")
            {
                fido.Talk();
                _talking = false;
            }
            else
            {
                Console.WriteLine("wrong input\nType any key to try again...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
