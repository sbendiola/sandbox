using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Sandbox.Spec
{
   /* [TestFixture]
    public class NullSafeTest
    {
        [Test]
        public void WhenAccesingNullElementShouldNotFail()
        {
            int result = new NullSafe<Parent>(null).Apply(delegate (Parent p) { return p.age; } );
            Assert.AreEqual(0, result);
        }

        [Test]
        public void WhenAccesingNullCollectionElementShouldNotFail()
        {
            int result = new NullSafe<Parent>(null).Apply(delegate(Parent p) { return p.age; });
            Assert.AreEqual(0, result);
        }
    }

    internal class NullSafe<T, R> where T : class where R : class
    {
        private readonly T it;
        private readonly R value;

        public NullSafe(T it) : this(it, it as R)
        {
            
        }

        private NullSafe(T it, R ret)
        {
            this.it = it;
            this.value = ret;
        }

        public R Value
        {
            get { return value; }
        }

        public NullSafe<V, V> Apply<V>(Func<T, V> func) where V : class
        {
            if (it != null)
            {
                return new NullSafe<V, V>(func(it));
            }
            return new NullSafe<V, V>(null as V);
        }

        public NullSafe<int, int> Apply(Func<T, int> func) 
        {
            int ret = 0;
            if (it != null)
            {
                return func(it);
            }
            return ret;
        }

    }

    public delegate R Func<T, R>(T t);
    
    public class Parent : Child
    {
        public readonly List<Child> children = new List<Child>();
        
    }

    public class Child
    {
        public readonly int age;
        public readonly DateTime birth;
        
    }*/
}