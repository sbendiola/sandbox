using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Sandbox
{
    [TestFixture]
    public class ArrayTest
    {
        [Test]
        public void ToConvertFromList()
        {
            List<string> strings = new List<string>();
            strings.Add("foo");
            strings.Add("bar");
            List<object> objects = new List<object>((object[]) strings.ToArray());
    
            Assert.AreEqual(2, objects.Count);
        }

        [Test]
        public void ArraysAreCovariant()
        {
            string[] strings = new string[]{ "1", "2"};
            object[] objects = strings;

            Assert.AreEqual(strings[0], objects[0]);
        }
    }
}