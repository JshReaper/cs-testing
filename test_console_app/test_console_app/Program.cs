using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_console_app
{
    internal static class Program
    {
        private static bool _talking = true;
        private static void Main(string[] args)
        {
            Console.Write("<<generic stuff>> ");
            Console.WriteLine("Hello World");
            while (_talking)
            {
                EnCounterDog();
            }
            Console.WriteLine("oh look a fox");
            var foxi = new Fox();
            foxi.Talk();
            Console.ReadKey();
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

    internal abstract class Animal
    {
        public virtual void Eat()
        {
            throw new NotImplementedException();
        }

        public virtual void Move()
        {
            throw new NotImplementedException();
        }

        public virtual void Talk()
        {
            Console.WriteLine("generic animal noise");
        }
    }

    internal abstract class Dog : Animal
    {
        public override void Talk()
        {
            Console.WriteLine("Wuuf");
        }
    }

    internal class Fox : Animal
    {

    }



    internal class GoldenRetriever : Dog
    {
    }
}
