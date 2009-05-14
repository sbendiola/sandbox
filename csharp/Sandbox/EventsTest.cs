using System;
using NUnit.Framework;

namespace Sandbox
{
    [TestFixture]
    public class EventsTest
    {
        [Test]
        public void EventCanBeBubbledUp()
        {
            Parent parent = new Parent();
            parent.child.OnClick();
            Assert.AreSame(parent.sender, parent.child);
        }

        public class Parent
        {
            public event EventHandler Hello = delegate { };
            public Child child = new Child();
            public object sender;
            public Parent()
            {
                child.Hello += Hello;
                child.Hello += DefaultHandler;
            }

            public void DefaultHandler(object sender, EventArgs args)
            {
                this.sender = sender;
            }

            
        }

        public class Child
        {
            public event EventHandler Hello = delegate { };


            public void OnClick()
            {
                Hello.Invoke(this, new EventArgs());
            }
        }
    }
}