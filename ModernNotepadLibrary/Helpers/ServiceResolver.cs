using System;
using System.Collections.Generic;
using System.Linq;

namespace ModernNotepadLibrary.Helpers
{
    public class ServiceResolver
    {
        private readonly Dictionary<Type, Type> types = new Dictionary<Type, Type>();

        public void Register<TInterface, TImplementation>() where TImplementation : TInterface 
            => types[typeof(TInterface)] = typeof(TImplementation);

        public TInterface Create<TInterface>() => (TInterface)Create(typeof(TInterface));

        private object Create(Type type)
        {
            var concreteType = types[type];
            var defaultConstructor = concreteType.GetConstructors()[0];
            var defaultParams = defaultConstructor.GetParameters();

            var parameters = defaultParams.Select(param => Create(param.ParameterType));

            return defaultConstructor.Invoke(parameters.ToArray());
        }
    }
}
