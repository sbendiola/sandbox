using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Sandbox.props
{
    [TestFixture]
    public class PropertiesTest
    {
        [Test]
        public void OverrideProperties()
        {
            
        }

    }

    public class Foo
    {
        private int bar;

        public virtual int Bar
        {
            get { return bar;}
        }
    }
}
