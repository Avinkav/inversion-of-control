using System;

namespace di_basic
{
    class Program
    {
        static void Main(string[] args)
        {

            IProvider provider = new ServiceA(); // or ServiceB
            var objectA = new ObjectA(provider);

            objectA.DoSomething();
        }
    }

    class ObjectA
    {
        private readonly IProvider _provider;

        public ObjectA(IProvider provider)
        {
            _provider = provider;
        }

        public void DoSomething()
        {
            _provider.UseMe();
        }
    }

    public interface IProvider
    {
        void UseMe();
    }

    class ServiceA : IProvider
    {
        public void UseMe()
        {
            Console.WriteLine(@"I'm Service A and I'm being used to print two lines.
Object A has no idea I printed two lines. Neither do I know who Object A is");
        }
    }

    class ServiceB : IProvider
    {
        public void UseMe()
        {
            Console.WriteLine("I'm Service B. I'm lazy. I print one.");
        }
    }
}





