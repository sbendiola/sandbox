using System;
using System.Collections.Generic;
using System.Text;
using Rhino.Mocks;

using NUnit.Framework;

namespace Events
{
    public delegate void MyEventHandler(object sender, String message);

    public class FooWithDefault {
        public event MyEventHandler handler = delegate { };
        public void Notify()
        {
            handler(this, "foo");
        }
    };

    public class FooWithoutDefault
    {
        public event MyEventHandler handler;
        public void Notify()
        {
            handler(this, "foo");
        }
    };


    [TestFixture]
    public class DefaultDelegateForEventHandler
    {

        
        [Test]
        public void ShouldInvokeMock()
        {
            FooWithDefault x = new FooWithDefault();
            MockRepository repository = new MockRepository();
            MyEventHandler mockDelegate = repository.CreateMock<MyEventHandler>();
            mockDelegate.Invoke(x, "foo");

            repository.ReplayAll();
            x.handler += mockDelegate;
            
            x.Notify();
            repository.VerifyAll();
        }

        [Test]
        public void ShouldNotBlowUp()
        {
            FooWithDefault x = new FooWithDefault();
            x.Notify();
        }

        [Test]
        public void ShouldBlowUp()
        {
            try
            {
                FooWithoutDefault x = new FooWithoutDefault();
                x.Notify();    
                Assert.Fail("should have thrown an exception since the handler is not set");
            } catch(NullReferenceException expected)
            {
            }
            
        }

        public void Func(object sender, string message)
        {
            System.Console.Out.WriteLine("sender:" + sender + " message:" + message);
        }

    }
}
