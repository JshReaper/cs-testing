using System;

namespace test_console_app
{
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
}