using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Sandbox.Struct
{
    [TestFixture]
    public class TestStructure
    {
        [Test]
        public void AssignmentChangesInstance()
        {
            FooBar original;
            original.foo = "123";
            original.bar = 23;
            FooBar first = original;
            original.foo = "223";
            FooBar second = original;
            Assert.AreNotSame(first, original);
            Assert.AreNotSame(first, second);

            PassByValue(original);
        }

        public void PassByValue(FooBar fb)
        {
            fb.foo = "";
        }


    }

    public struct FooBar
    {
        public string foo;
        public int bar;

    }
}
