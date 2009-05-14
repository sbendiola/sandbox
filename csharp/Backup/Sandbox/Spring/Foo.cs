using System;
using System.Collections.Generic;
using System.Text;

namespace Sandbox.Spring
{
    public class Foo
    {
        private readonly string name;
        private int age;

        public Foo(string name)
        {
            this.name = name;
        }

        public Foo(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public string Name
        {
            get { return name; }
        }
    }
}
