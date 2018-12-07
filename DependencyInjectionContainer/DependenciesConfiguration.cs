using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace DependencyInjectionContainer
{
    public class DependenciesConfiguration
    {
        public ConcurrentDictionary<Type, List<Type>> _dictionary {get; }

        public DependenciesConfiguration()
        {
            _dictionary = new ConcurrentDictionary<Type, List<Type>>();
        }

        public bool Register<TDependency, TImplementation>() where TDependency : class where TImplementation : class
        {
            return Register(typeof(TDependency), typeof(TImplementation));
        }

        public bool Register(Type tDependency, Type tImplementation)
        {
            if (!tImplementation.IsInterface && !tImplementation.IsAbstract)
            {
                if (!_dictionary.ContainsKey(tDependency))
                {
                    _dictionary.TryAdd(tDependency, new List<Type>());
                }
                if (!_dictionary[tDependency].Contains(tImplementation))
                {
                    _dictionary[tDependency].Add(tImplementation);
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
