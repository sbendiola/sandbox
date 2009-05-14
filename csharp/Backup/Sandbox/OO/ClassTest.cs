using System;
using System.Reflection;
using NUnit.Framework;

namespace Sandbox.OO
{
    [TestFixture]
    public class ClassTest
    {
        [Test]
        public void MethodsAreNotVirtualByDefauault()
        {
            MethodInfo method = typeof(Foo).GetMethod("bar", new Type[]{});
            Assert.AreEqual(false, method.GetBaseDefinition().IsVirtual);
        }

        [Test]
        public void CanChangeMethodBehaviorThroughStaticType()
        {
            Foo foo = new Foo();
            Foo2 foo2 = new Foo2();

            Assert.AreEqual(1, foo.bar());
            Assert.AreEqual(2, foo2.bar());
            foo = foo2;
            Assert.AreEqual(1, foo.bar());
        }

        [Test]
        public void ChainedMethodCallsCanChangeMethodBehaviorThroughStaticType()
        {
            Foo foo = new Foo();
            Foo2 foo2 = new Foo2();

            Assert.AreEqual(2, foo.bar2());
            Assert.AreEqual(3, foo2.bar2());

            foo = foo2;
            Assert.AreEqual(3, foo.bar2());
        }


        public class Foo
        {
            public int bar()
            {
                return 1;    
            }

            public int bar2()
            {
                return overrideMe() + 1;
            }

            public virtual int overrideMe()
            {
                return 1;
            }
        }

        public class Foo2 : Foo
        {
            public new int bar()
            {
                return 2;
            }

            public override int overrideMe()
            {
                return 2;
            }
        }
    }
}