using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;

namespace Sandbox.Mock
{

    [TestFixture]
    public class MockUsage
    {
        [Test]
        public void OldStyle()
        {
            var mockery = new MockRepository();
            
        }
        


    }

    public class Foo
    {
        public void Save(Bar bar)
        {
            
        }
    }

    public class Bar
    {
        
    }
}
