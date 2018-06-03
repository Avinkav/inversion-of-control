using System;

namespace dotnet_c_sharp
{
    class Program
    {
        static IocContainer serviceLocator = IocContainer.Default;

        static void Main(string[] args)
        {
            Console.WriteLine("Beginning Dependeny Injection Demo\n");
           
            Startup();

            Console.WriteLine("Demonstrating deep dependency injection...\n");
            
            // Resolving consumer A requires MathService to be resolved which in turn requires
            // BeautyService to be resolved. The container instantiates 3 seperate objects
            // when the following resolve is called
            var consumerA = serviceLocator.Resolve<ConsumerA>();
            var consumerB = serviceLocator.Resolve<ConsumerB>();
            
            // Consumer A uses math service
            // Math service does it's own processing and also uses BeautyService internally
            consumerA.UseMathService(10);

            // Consumer B uses math service
            // The resulting output shows that consumer B also has used the same Math service
            // Consumer A user
            consumerB.Subtract(5);

            
            Console.WriteLine("Demonstrating the singleton concept... \n");

            consumerA.SetMySecret("I'm a banana");
            consumerA.WhatsMySecret();

            // Resolving consumer A again
            var anotherReferenceToConsumerA = serviceLocator.Resolve<ConsumerA>();

            // Turns out the secret is still  the same, meaning that the object is the same 
            // as before
            anotherReferenceToConsumerA.WhatsMySecret();

            // Changing the secret using the new reference
            anotherReferenceToConsumerA.SetMySecret("I'm a monkey");

            // Accessing the old reference, hold on
            // Am I a monkey now?
            consumerA.WhatsMySecret();
        }

        private static void Startup(){
            Console.WriteLine("Framework start up routine");
            serviceLocator.Register<ConsumerB>();
            serviceLocator.Register<MathService>();
            serviceLocator.Register<ConsumerA>();
            serviceLocator.Register<BeautyService>();
            Console.WriteLine();
        }
    }
}
