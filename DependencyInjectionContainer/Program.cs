using System;
using DependencyInjectionContainer.Exceptions;

namespace DependencyInjectionContainer
{   
    class Program
    {
        static void Main(string[] args)
        {
            DependenciesConfiguration c = new DependenciesConfiguration();
            c.Register<ClassForExample, ClassForExample>(true);
            c.Register<ClassForExample2, ClassForExample2>(true);
            c.Register<ClassForExample3, ClassForExample3>(false);
            DependencyProvider p = new DependencyProvider(c);
            try
            {
                ClassForExample example = p.Resolve<ClassForExample>();
                example.Print();
            }
            catch(ConfigurationValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }catch(ConstructorNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }catch (CycleDependencyException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
