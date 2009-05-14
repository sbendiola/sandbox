using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Sandbox.Reflection
{
    [TestFixture]
    public class DelegateReflectionTest
    {
        public delegate void FooBar(string str);
        public delegate void FooBar2(string str);

        public delegate object ReturnsObject(string str);
        public delegate string ReturnsString(string str);

        [Test]
        public void DelegateInfoCanBeDiscoveredFromReflection()
        {
            Type type = typeof (FooBar);
            Type type2 = typeof(FooBar2);
            FooBar2 d = doIt;

            Type type3 = d.GetType();
            


            Assert.AreNotEqual(type, type2);
            Assert.AreEqual(type3, type2);
            Assert.AreNotEqual(type, type3);

        }


        [Test]
        public void DelegateReturnTypesShouldBeCovariant()
        {
            ReturnsObject r0 = delegate (string str) { return "123"; };
            ReturnsObject r1 = delegate(string str) { return new List<string>(); };
            
        }

        public void doIt2(Delegate it)
        {

        }

        public void doIt(string it)
        {

        }
    }

    class Other
    {
        public void Foo()
        {
            DelegateReflectionTest.FooBar b = doIt;
            //doIt2(DelegateReflectionTest.FooBar);

        }

        public void doIt2(Delegate it)
        {

        }

        public void doIt(string it)
        {
            
        }
    }
}