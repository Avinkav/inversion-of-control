using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace dotnet_c_sharp
{
    // This is a lazy loaded Inversion of Control Container
    // It will only instantiate types when necessary
    public class IocContainer
    {
        // The  container that stores the types and objects associated with the types
        private IDictionary<Type, Object> container = new Dictionary<Type, Object>();

        public void Register<T>()
        {
            var type = typeof(T);
            Console.WriteLine($"Registering singleton type: {type}");

            // We do not instantiate types at time of registration.
            // We add a null to the container to indicate it hasn't been instantiated yet.
            // This allows for services to be registered in any order.
            container.Add(type, null);
        }

        public void DeRegister<T>()
        {
            var type = typeof(T);

            if (container.ContainsKey(type))
            {
                Console.WriteLine($"Removing Type {type}");

                //  Releasing the reference to the object doesn't guarantee destruction, any
                // any dependencies that were resolved before this will still hold the reference
                container.Remove(type);
            }
            else
                // We are strictly enforcing type registration by throwing exceptions,
                throw new Exception($"Attempted to de-register unregistered type: {type}");
        }

        // Accepts a type T and converts it into a Type object before calling the private recursive
        // method that does the resolving
        public T Resolve<T>()
        {
            var type = typeof(T);
            return (T)Resolve(type);
        }

    private object Resolve(Type type)
    {
        // We only resolve types that are registered against the container
        if (!container.ContainsKey(type))
            throw new Exception($"Register type {type} first before resolving");

        // Instantiate when needed. ie. Lazily
        if (container[type] == null)
        {
            // This IoC Container does not provide the luxury of multiple constructors
            // It will always use the first constructor to instantiate an object
            var ctor = type.GetConstructors().First();

            // We get parameters of that constructor using GetParamters(),
            // The linq Select method allows us to project the list of parameters to a list of
            // instantiated objects that are recursivelly resolved using this very method
            // ToArray() as we need an array to pass to the Activator that creates objects
            var param = ctor.GetParameters().Select(p => Resolve(p.ParameterType)).ToArray();

            // Instantiating object and storing it in the container
            container[type] = Activator.CreateInstance(type, args: param);
        }
        return container[type];
    }

        // We expose a default instance of this class for the application to use
        public static IocContainer Default = new IocContainer();
    }
}