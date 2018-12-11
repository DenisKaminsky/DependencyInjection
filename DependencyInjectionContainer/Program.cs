using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionContainer
{
    public class class1:interf
    {
        public void Print()
        {
            Console.WriteLine("i am class1");
        }
    }

    public class class2:interf
    {
        public void Print()
        {
            Console.WriteLine("i am class2");
        }
    }

    public abstract class class3
    {

    }

    public interface interf
    {
        void Print();
    }

    class Program
    {
        static void Main(string[] args)
        {
            DependenciesConfiguration c = new DependenciesConfiguration();
            c.Register<interf, class1>(true);
            c.Register<interf, class2>(false);
            DependencyProvider p = new DependencyProvider(c);
            IEnumerable<interf> res = p.Resolve<IEnumerable<interf>>();
            IEnumerable<interf> res2 = p.Resolve<IEnumerable<interf>>();

            if (res.ElementAt(0) == res2.ElementAt(0))
                Console.WriteLine("Yes. It is really singleton");
            if (res.ElementAt(1) == res2.ElementAt(1))
                Console.WriteLine("Yes. It is really singleton");
            else
                Console.WriteLine("No. It is not singleton");

        }
    }
}
