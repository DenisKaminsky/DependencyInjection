using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace DependencyInjectionContainer
{
    public class DependenciesConfiguration
    {
        public ConcurrentDictionary<Type, List<Type>> dependencies {get; }
        public ConcurrentDictionary<Type, List<bool>> isSingletonDictionary { get;}

        public DependenciesConfiguration()
        {
            dependencies = new ConcurrentDictionary<Type, List<Type>>();
            isSingletonDictionary = new ConcurrentDictionary<Type, List<bool>>();
        }

        public void Register<TDependency, TImplementation>(bool isSingleton)
        {
            Register(typeof(TDependency), typeof(TImplementation),isSingleton);
        }

        public void Register(Type tDependency, Type tImplementation,bool isSingleton)
        {
            dependencies.TryAdd(tDependency, new List<Type>());
                
            if (!dependencies[tDependency].Contains(tImplementation))
            {
                dependencies[tDependency].Add(tImplementation);
                isSingletonDictionary[tDependency].Add(isSingleton);
            }          
        }
    }
}
