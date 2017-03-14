using System;

namespace test_console_app
{
    internal abstract class Dog : Animal
    {
        public override void Talk()
        {
            Console.WriteLine("Wuuf");
        }
    }
}