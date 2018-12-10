using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionContainer
{
    public class class1:interf
    {

    }

    public class class2:interf
    {

    }

    public abstract class class3
    {

    }

    public interface interf
    {

    }

    class Program
    {
        static void Main(string[] args)
        {
            DependenciesConfiguration c = new DependenciesConfiguration();
            c.Register<interf, class1>(true);
            c.Register<interf, class2>(true);
            DependencyProvider p = new DependencyProvider(c);
            IEnumerable<interf> res = p.Resolve<IEnumerable<interf>>();
        }
    }
}
