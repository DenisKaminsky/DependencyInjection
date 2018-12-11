using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionContainer
{
    public class DependencyProvider
    {
        private DependenciesConfiguration _configuration;
        private readonly ConcurrentStack<Type> _stack;

        public DependencyProvider(DependenciesConfiguration configuration)
        {
            if (ValidateConfiguration(configuration))
            {
                _configuration = configuration;
                _stack = new ConcurrentStack<Type>();
            }
            else
            {
                throw new Exception("Configuration is not valid!");
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
            else
            {
                return (T)GetInstance(t);
            }
        }

        private object CreateGeneric(Type t)
        {
            object result = null;
            List<Type> implementations;
            Type tResolve = t.GetGenericArguments()[0];
            
            _configuration.dependencies.TryGetValue(tResolve,out implementations);
            if (implementations != null)
            {
                result = Activator.CreateInstance(typeof(List<>).MakeGenericType(tResolve));
                foreach (Type tImplementation in implementations)
                {
                    //add
                }
            }
            return result;           
        }
        
        //here will be validation for singleton
        private object GetInstance(Type t)
        {

        }
                
        //t - is implementation type
        private object Create(Type t)
        {
            if (!_stack.Contains(t))
            {
                _stack.Push(t);

                /*if (t.IsGenericTypeDefinition)
                {
                    t = t.MakeGenericType(t.GenericTypeArguments);
                }*/

                var constructors = t.GetConstructors().OrderByDescending
                    (x => x.GetParameters().Length).ToArray();
                
                _stack.TryPop(out t);
            }
            else
            {
                throw new Exception("Cycle dependency ERROR!");
            }
        }

        private ConstructorInfo GetRightConstructor(Type t)
        {
            ConstructorInfo result = null;
            ConstructorInfo[] constructors = t.GetConstructors();
            bool isRight;

            foreach (ConstructorInfo constructor in constructors)
            {
                ParameterInfo[] parameters = constructor.GetParameters();

                isRight = true;
                foreach (ParameterInfo parameter in parameters)
                {
                    if (!_configuration.dependencies.ContainsKey(parameter.ParameterType))
                    {
                        isRight = false;
                        break;
                    }
                }

                if (isRight)
                {
                    result = constructor;
                    break;
                }
            }
            return result;
        }

        private ParameterInfo[] GetConstructorParameters()
        {

        }

    }
}
