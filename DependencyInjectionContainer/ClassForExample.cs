using System;

namespace DependencyInjectionContainer
{
    public interface IRepository
    {
        void Print();
    }

    public class MySQLRepository: IRepository
    {
        public void Print()
        {
            Console.WriteLine("THIS IS MYSQL");
        }
    }

    public interface IService<TRepository> where TRepository : IRepository
    {
        void INVOKE();
    }

    public class ServiceImpl<TRepository> : IService<TRepository>
    where TRepository : IRepository
    {
        TRepository rep;
        public ServiceImpl(TRepository repository)
        {
            rep = repository;
        }

        public void INVOKE()
        {
            rep.Print();
        }
    }

    public interface IExample
    {
        void Print();
    }

    public class ClassForExample:IExample
    {
        public ClassForExample2 example { get; set; }

        public ClassForExample(ClassForExample2 example)
        {
            this.example = example;
        }

        public ClassForExample()
        {
        }

        public void Print()
        {
            Console.WriteLine("I am EXAMPLE 1");
            if (example != null)
                example.Print();
        }
    }

    public class ClassForExample66<T> where T:IExample
    {
        public T example { get; set; }

        public ClassForExample66(T example)
        {
            this.example = example;
        }

        public void Print()
        {
            Console.WriteLine("I am EXAMPLE 66");
            if (example != null)
                example.Print();
        }
    }

    public class ClassForExample2:IExample
    {
        public ClassForExample3 example { get; set; }

        public ClassForExample2(ClassForExample3 example)
        {
            this.example = example;
        }

        public ClassForExample2()
        {
        }

        public void Print()
        {
            Console.WriteLine("I am EXAMPLE 2");
            if (example != null)
                example.Print();
        }
    }

    public class ClassForExample3
    {
        public ClassForExample example { get; set; }

        public ClassForExample3(ClassForExample example)
        {
            this.example = example;
        }

        public ClassForExample3()
        {
        }

        public void Print()
        {
            Console.WriteLine("I am EXAMPLE 3 with link to EXAMPLE 1");
        }
    }

}
