using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Spring.Context;
using Spring.Context.Support;

namespace Sandbox.Spring
{
    [TestFixture]
    public class ObjectFactoryTest
    {
        [Test]
        public void ShouldCreateInstanceWithArgs()
        {
            IApplicationContext context = new XmlApplicationContext("file://Spring/with-args.xml");
            using(context)
            {
                object o = context.GetObject("b", new object[] {"string", 13});
                Foo foo = o as Foo;
                Assert.IsNotNull(foo);
                Assert.AreEqual(13, foo.Age);
                Assert.AreEqual("string", foo.Name);
            }
        }
    }
}
