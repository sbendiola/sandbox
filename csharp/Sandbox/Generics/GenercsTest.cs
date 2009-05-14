using System;
using NUnit.Framework;

namespace Sandbox.Generics
{
    [TestFixture]
    public class GenercsTest
    {
        [Test]
        public void ShouldBeAbleToDeclareBaseTypeOnMethod()
        {
             

        }

        [Test]
        public void GenericTypeShouldBeAvailableAtRuntime()
        {
            Foo f = create<Foo>();
            Assert.IsNotNull(f);

        }

        private T create<T>() where T : new()
        {
            return new T();    
        }

        private void MethodWithRestrictionOnArgument<X>(X x) where X : IComparable<X>
        {
            
        }

        private void MethodCanOnlyTakeOneRestrictionPerType<X, Y>(X x) where X : Bar where Y : Foo
        {

        }

        private void CanAbstractDefaultConstructor<X>(X x) where X : new()            
        {
            CanAbstractDefaultConstructor(new Foo());
            //doesn't compile - CanAbstractDefaultConstructor(new TwoParams("x"));
        }

        //doesn't compile - 
        /*private void CantAbstractConstructorWithParam<X>(X x) where X : new(string)
        {

        }*/

        public class TwoParams
        {
            public TwoParams(string x)
            {
                
                MethodCall call = new MethodCall();
               // call.Call(this.FooBar("x")).Returns("wdrr");
                
            }

            public int FooBar(String x)
            {
                return 1;
            }

            public int FooBar(String x, int t)
            {
                return 1;
            }
           

        }

        public delegate R Func<A, R>(A arg);
        public delegate R Func<A1, A2, R>(A1 arg1, A2 arg2);

        public class ReturnValue<T>
        {
            public void Returns(T t)
            {
                
            }
        }
        public class MethodCall
        {
            public ReturnValue<R> Call<R>(R r)
            {
                return new ReturnValue<R>();
            }

            
        }

        
        class Foo
        {
            public void foo()
            {
                
            }
        }

        class Bar
        {
            public void bar()
            {
                
            }
        }

        class GenericListKnowsType<T> where T : Bar
        {
            private T t = null;
            public void Go()
            {
                t.bar();
            }
        }
    }
}