using System;
using System.Collections.Generic;
using DependencyInjectionContainer.Exceptions;

namespace DependencyInjectionContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            DependenciesConfiguration c = new DependenciesConfiguration();
            //c.Register<IExample, IExample>(false);
            c.Register(typeof(IService<>), typeof(ServiceImpl<>),false);

            //c.Register<ClassForExample, ClassForExample>(true);
            //c.Register<ClassForExample66<ClassForExample>,ClassForExample66<ClassForExample>>(true);
            //c.Register<ClassForExample2, ClassForExample2>(true);
            //c.Register<ClassForExample3, ClassForExample3>(false);
            try
            {
                DependencyProvider p = new DependencyProvider(c);
                IService<MySQLRepository> example = p.Resolve<IService<MySQLRepository>>();
                //ClassForExample66<ClassForExample> example = p.Resolve<ClassForExample66<ClassForExample>>();
                example.INVOKE();
            }
            catch (ConfigurationValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ConstructorNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (CycleDependencyException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (TypeNotRegisterException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
