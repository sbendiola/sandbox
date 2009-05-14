using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace Sandbox.Generics
{
    [TestFixture]
    public class ErasureTest
    {

     [Test]   
     public void ChainedCallsShouldWork()
     {
         
     }

    [Test]
    public void ListsAreNotCovariant()
    {
        IList strings = new List<string>();
        List<object> obj = new List<object>();
        
        //does not compile obj = strings;
        IEnumerator<String> estr = null;
        IEnumerator<object>  eo = null;

      /*  AContainer container = new AContainer();
        container.Add(1);
        container.Add("1");


        foreach (int i in container)
        {
            Console.WriteLine(i);
        }

        foreach (string i in container)
        {
            Console.WriteLine(i);
        }*/
//        container.GetEnumerator()
        
    }

        public class AContainer : IContainer<String>, IContainer<int>, IContainer<object>
        {
            private Container<String> strings = new Container<string>();
            private Container<int> ints = new Container<int>();
            private Container<object> objects = new Container<object>();

            public void Add(string t)
            {
                strings.Add(t);
            }

            public void Remove(string t)
            {
                strings.Remove(t);
            }

            IEnumerator<string> IContainer<string>.GetEnumerator()
            {
                return strings;
            }


            public void Add(int t)
            {
                ints.Add(t);
            }

            public void Remove(int t)
            {
                throw new NotImplementedException();
            }

            IEnumerator<int> IContainer<int>.GetEnumerator()
            {
                return ints;
            }

            public void Add(object t)
            {
                throw new NotImplementedException();
            }

            public void Remove(object t)
            {
                throw new NotImplementedException();
            }

            public IEnumerator<object> GetEnumerator()
            {
                return objects;
            }
        }


        public interface  IContainer<T>
        {
            void Add(T t);
            void Remove(T t);
            IEnumerator<T> GetEnumerator();
        }

        public class Container<T> : IEnumerator<T>
        {
            private IList<T> list = new List<T>();

  
            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public bool MoveNext()
            {
                throw new NotImplementedException();
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }

            public object Current
            {
                get { throw new NotImplementedException(); }
            }

            T IEnumerator<T>.Current
            {
                get { throw new NotImplementedException(); }
            }

            public void Add(T t)
            {
                list.Add(t);
            }

            public void Remove(T t)
            {
                list.Add(t);
            }

        }


        /*public class EnumerableGeneric<TClass, TInterface> : IEnumerable<TInterface> where TClass : TInterface
        {
            private IList<TClass> list;

            public EnumerableGeneric(IList<TClass> list)
            {
                this.list = list;
            }

            public IEnumerator<TInterface> GetEnumerator()
            {
                foreach (TClass item in list)
                    yield return item;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        } */
        


        class Factory<T> where T : new()
        {
            public T create()
            {
                return new T();
            }
        }
    }
}