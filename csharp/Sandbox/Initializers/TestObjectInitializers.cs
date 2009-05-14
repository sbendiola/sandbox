using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Sandbox.Initializers
{
    [TestFixture]
    public class TestObjectInitializers
    {
        [Test]
        public void ConstructorInvokedFirst()
        {
            new Elmi {Foo = "123", Boo = "abc"};
        }


        public class Elmi
        {
            private String foo;
            public Elmi()
            {
                Console.WriteLine("creating");
            }
    
            public String Foo
            {
                get { return foo; }
                set
                {
                    Console.WriteLine("setting");
                    foo = value;
                }
            }

            public String Boo { get; set; } 
        }
    }

    
}
