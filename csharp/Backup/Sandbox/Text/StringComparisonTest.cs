using System;
using System.Text;
using NUnit.Framework;

namespace Sandbox.Text
{
    [TestFixture]
    public class StringComparisonTest
    {
        [Test]
        public void FirstParamNullCanBeCompared()
        {
            string.Compare(null, "123");
        }

        [Test]
        public void SecondParamNullCanBeCompared()
        {
            string.Compare("123", null);
        }

        [Test]
        public void NothNullCanBeCompared()
        {
            string.Compare(null, null);
        }

        [Test]
        public void InstanceCompare()
        {
            string foo = null;
            foo.Equals(null);
        }

        [Test]
        public void EqualsShouldBeTheSameAsEq()
        {
            // Create two equal but distinct strings
            string a = new string(new char[] { 'h', 'e', 'l', 'l', 'o' });
            string b = new string(new char[] { 'h', 'e', 'l', 'l', 'o' });

            Assert.IsTrue(a == b);
            Assert.IsTrue(a.Equals(b));

            object c = a;
            object d = b;

            Assert.IsFalse(c == d);
            Assert.IsTrue(c.Equals(d));

            if(new Foo())
            {
                
            }
            else
            {
                Assert.Fail("should fail");
            }

            Foo foo = new Foo();
            
        }


        class Foo
        {

            public static bool operator true(Foo other)
            {
                return false;
            }

            public static bool operator false(Foo other)
            {
                return true;
            }
            
            public static bool operator +(Foo other)
            {
                return true;
            }

            public static bool operator !=(Foo other, Foo foo)
            {
                return true;
            }

            public static bool operator ==(Foo other, Foo foo)
            {
                return true;
            }

            public override bool Equals(object obj)
            {
                return base.Equals(obj);
            }

        }
    }
}