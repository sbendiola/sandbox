using NUnit.Framework;
using Spring.Context;
using Spring.Context.Support;

namespace Sandbox.Spring
{
    [TestFixture]
    public class PropertyConfigurationTest
    {
        [Test]
        public void OverridePropertiesShouldBeSet()
        {
            IApplicationContext context = new XmlApplicationContext("Spring/dev.xml");
            Foo a = context["a"] as Foo;
            Assert.IsNotNull(a);

            Assert.AreEqual(1, a.Age);
            Assert.AreEqual("name", a.Name);
        }
    }
}