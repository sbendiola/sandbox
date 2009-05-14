using System;
using NUnit.Framework;
using Rhino.Mocks;

namespace Sandbox.Equality
{
    [TestFixture]
    public class EqualityTest
    {
        [Test]
        public void EqEqShouldNotCallEquals()
        {
            Foo foo = new Foo();
            object x = "123";
            Assert.IsFalse(x == foo);
            Assert.IsFalse(called);
            foo.Equals("123");
            Assert.IsTrue(called);

        }

        public static bool called = false;
    }
    
    public class Foo {
        
        public override bool Equals(object obj)
        {
            EqualityTest.called = true;
            return base.Equals(obj);
        }
    }
}