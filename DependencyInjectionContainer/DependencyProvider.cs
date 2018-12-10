using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionContainer
{
    public class DependencyProvider
    {
        private DependenciesConfiguration _configuration;

        public DependencyProvider(DependenciesConfiguration configuration)
        {
            if (ValidateConfiguration(configuration))
            {
                _configuration = configuration;
            }
            else
            {
                throw new Exception("Configuration is not valid");
            }
        }

        private bool ValidateConfiguration(DependenciesConfiguration configuration)
        {
            foreach (Type tDependency in configuration.dependencies.Keys)
            {
                if (!tDependency.IsValueType)
                {
                    foreach (Type tImplementation in configuration.dependencies[tDependency])
                    {
                        if (tImplementation.IsAbstract || tImplementation.IsInterface || !tDependency.IsAssignableFrom(tImplementation))
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public T Resolve<T>() where T: class
        {
            Type t = typeof(T);

            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                return (T)CreateGeneric(t);
            }
        }

        private object CreateGeneric(Type t)
        {
            List<Type> implementations;
            Type tResolve = t.GetGenericArguments()[0];
            
            _configuration.dependencies.TryGetValue(tResolve,out implementations);
            if (implementations != null)
            {
                var result = Activator.CreateInstance(typeof(List<>).MakeGenericType(tResolve));
                foreach (Type tImplementation in implementations)
                {
                    //add
                }
                return result;
            }
            return null;           
        }
    }
}
