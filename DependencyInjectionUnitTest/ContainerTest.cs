﻿using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DependencyInjectionContainer;
using DependencyInjectionContainer.Exceptions;

namespace DependencyInjectionUnitTest
{
    [TestClass]
    public class ContainerTest
    {
        //just create dependency
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

        //compare objects links, returned by provider
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

        //compare objects links, returned by provider
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

        //implementation == dependency
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

        //return some implementations of one dependency
        [TestMethod]
        public void GetSomeImplementationsTest()
        {
            DependencyProvider provider;
            DependenciesConfiguration config = new DependenciesConfiguration();

            config.Register<IExample, ClassForExample>(true);
            config.Register<IExample, ClassForExample2>(false);
            provider = new DependencyProvider(config);
            IEnumerable<IExample> actual = provider.Resolve<IEnumerable<IExample>>();

            Assert.IsNotNull(actual);
            Assert.AreEqual(2, ((IList)actual).Count);
        }

        [TestMethod]
        public void ImplementationIsInterfaceTest()
        {
            DependencyProvider provider;
            DependenciesConfiguration config = new DependenciesConfiguration();

            config.Register<IExample, IExample>(true);
            try
            {
                provider = new DependencyProvider(config);
                IExample actual = provider.Resolve<IExample>();
                Assert.IsNotNull(actual);
            }
            catch (ConfigurationValidationException ex)
            {
                Assert.IsNotNull(ex.Message);
            }
        }

        [TestMethod]
        public void NotRegisteredTypeTest()
        {
            DependencyProvider provider;
            DependenciesConfiguration config = new DependenciesConfiguration();

            config.Register<IExample, ClassForExample>(true);
            try
            {
                provider = new DependencyProvider(config);
                ClassForExample2 actual = provider.Resolve<ClassForExample2>();
                Assert.IsNotNull(actual);
            }
            catch (TypeNotRegisterException ex)
            {
                Assert.IsNotNull(ex.Message);
            }
        }


    }
}
