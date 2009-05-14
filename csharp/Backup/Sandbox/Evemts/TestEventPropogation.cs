using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;

namespace Sandbox.Evemts
{
    [TestFixture]
    public class TestEventPropogation
    {
        [Test]
        public void OrderOfSubscribingImportantIfDirectDelegation()
        {
            B b = new B();
            A a = new A();
            
            
            bool called = false;
            a.Subscribe(b);    
            a.OnAction += delegate { called = true; };
            
            b.Notify("foo");
            Assert.IsFalse(called);
        }

        [Test]
        public void ShouldPropogateEventWhenSubscribeWithDelegate()
        {
            B b = new B();
            A a = new A();

            
            bool called = false;
            a.OnAction += delegate { called = true; };
            a.SubscribeWithDelegate(b);
            b.Notify("foo");
            Assert.IsTrue(called);
        }


        public class A
        {
            public event Action<string> OnAction = delegate { };

            public void Subscribe(B b)
            {
                b.OnAction += OnAction;
            }

            public void SubscribeWithDelegate(B b)
            {
                b.OnAction += delegate(string text) { OnAction(text); };
            }

            public void UnSubscribe(B b)
            {
                b.OnAction -= OnAction;
            }

            public void Notify(string text)
            {
                OnAction(text);
            }

        }

        public class B
        {
            public event Action<string> OnAction = delegate { };

            public void Notify(string text)
            {
                OnAction(text);
            }
        }
    }
}
