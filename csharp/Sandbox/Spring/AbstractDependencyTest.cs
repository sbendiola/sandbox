using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using Spring.Context;
using Spring.Context.Support;

namespace Sandbox.Spring
{
    public abstract class AbstractDependencyTest
    {
        private readonly bool tearDown = false;
        protected IApplicationContext context;

        public AbstractDependencyTest(bool tearDown)
        {
            this.tearDown = tearDown;
        }

        public AbstractDependencyTest()
            : this(false)
        {
        }

        [SetUp]
        public void Setup()
        {
            if (context == null)
            {
                context = new XmlApplicationContext(ConfigLocations);
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (context != null && tearDown)
            {
                context.Dispose();
            }
        }

        [TestFixtureTearDown]
        public void FixtureSetup()
        {
            if (context != null)
            {
                context.Dispose();
            }
        }

        public virtual void OnSetup()
        {
        }

        protected void InjectDepenencies()
        {
            List<MethodInfo> methodsTakingOneArg = PublicSetterMethods();
            methodsTakingOneArg.ForEach(delegate(MethodInfo method)
                                            {
                                                Type desiredType = method.GetParameters()[0].ParameterType;
                                                IDictionary type = context.GetObjectsOfType(desiredType);
                                                Assert.IsTrue(type.Count > 0,
                                                              "could not find an object of type " + desiredType.FullName);
                                                Assert.AreEqual(1, type.Count,
                                                                "found more than one type of " + desiredType.FullName);
                                                object[] first = new object[1];
                                                type.Values.CopyTo(first, 0);
                                                method.Invoke(this, first);
                                            }
                );
        }

        private List<MethodInfo> PublicSetterMethods()
        {
            Type t = GetType();
            MethodInfo[] publicMethods = t.GetMethods(BindingFlags.Public | BindingFlags.Instance);
            return new List<MethodInfo>(publicMethods).FindAll(
                delegate(MethodInfo method) { return method.GetParameters().Length == 1 && method.Name.ToLower().StartsWith("set"); }
                );
        }

        public abstract string[] ConfigLocations { get; }
    }
}