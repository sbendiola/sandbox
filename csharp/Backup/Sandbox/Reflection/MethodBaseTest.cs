using System.Reflection;
using NUnit.Framework;

namespace Sandbox.Reflection
{
    [TestFixture]
    public class MethodBaseTest
    {
        private static string staticString = MethodBase.GetCurrentMethod().DeclaringType.FullName;
        private string instanceString = MethodBase.GetCurrentMethod().DeclaringType.FullName;

        [Test]
        public void TypeOfShouldReturnsExpected()
        {
            string expected = "Sandbox.Reflection.MethodBaseTest";
            string fromtype = typeof (Sandbox.Reflection.MethodBaseTest).FullName;
            Assert.AreEqual(expected, fromtype);
        }

        [Test]
        public void StaticShouldReturnsExpected()
        {
            string expected = "Sandbox.Reflection.MethodBaseTest";

            Assert.AreEqual(expected, staticString);
        }

        [Test]
        public void InstanceShouldReturnsExpected()
        {
            string expected = "Sandbox.Reflection.MethodBaseTest";

            Assert.AreEqual(expected, instanceString);
        }

        [Test]
        public void ShouldFindMethod()
        {
            try
            {
                GetType().GetMethod("Foo");    
                Assert.Fail();
            } catch(AmbiguousMatchException)
            {
                
            }
        }

        public void Foo()
        {
            
        }

        public void Foo(int x)
        {
            
        }
    }
}