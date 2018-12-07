using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace DependencyInjectionContainer
{
    public class DependenciesConfiguration
    {
        public ConcurrentDictionary<Type, List<Type>> dictionary {get; }

        public DependenciesConfiguration()
        {
            dictionary = new ConcurrentDictionary<Type, List<Type>>();
        }

        public bool Register<TDependency, TImplementation>() where TDependency : class where TImplementation : class
        {
            return Register(typeof(TDependency), typeof(TImplementation));
        }

        public bool Register(Type tDependency, Type tImplementation)
        {
            if (!tImplementation.IsInterface && !tImplementation.IsAbstract)
            {
                if (!dictionary.ContainsKey(tDependency))
                {
                    dictionary.TryAdd(tDependency, new List<Type>());
                }
                if (!dictionary[tDependency].Contains(tImplementation))
                {
                    dictionary[tDependency].Add(tImplementation);
                }
            }
            else
            {
                return false;
            }
            return true;            
        }
    }
}
