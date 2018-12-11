using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DependencyInjectionContainer;
using DependencyInjectionContainer.Exceptions;

namespace DependencyInjectionUnitTest
{
    [TestClass]
    public class ContainerTest
    {
        [TestMethod]
        public void SingletonTest()
        {
            DependencyProvider provider;
            DependenciesConfiguration config = new DependenciesConfiguration();

            config.Register<IExample, ClassForExample>(true);
            provider = new DependencyProvider(config);
            IExample expected = provider.Resolve<IExample>();
            IExample actual = provider.Resolve<IExample>();

            Assert.AreEqual(expected, actual);
        }
    }
}
