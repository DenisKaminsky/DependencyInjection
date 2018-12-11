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
        public void CreateDependencyTest()
        {
            DependencyProvider provider;
            DependenciesConfiguration config = new DependenciesConfiguration();

            config.Register<IExample, ClassForExample>(false);
            config.Register<IExample, ClassForExample2>(true);
            provider = new DependencyProvider(config);
            IExample actual = provider.Resolve<IExample>();

            Assert.IsNotNull(actual);
        }

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

        [TestMethod]
        public void InstancePerDependencyTest()
        {
            DependencyProvider provider;
            DependenciesConfiguration config = new DependenciesConfiguration();

            config.Register<IExample, ClassForExample>(false);
            provider = new DependencyProvider(config);
            IExample expected = provider.Resolve<IExample>();
            IExample actual = provider.Resolve<IExample>();

            Assert.AreNotEqual(expected, actual);
        }      

        [TestMethod]
        public void AsSelfRegistrationTest()
        {
            DependencyProvider provider;
            DependenciesConfiguration config = new DependenciesConfiguration();

            config.Register<ClassForExample, ClassForExample>(true);
            provider = new DependencyProvider(config);
            ClassForExample actual = provider.Resolve<ClassForExample>();

            Assert.IsNotNull(actual);
        }
    }
}
