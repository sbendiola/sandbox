using System;
using System.Diagnostics;
using System.Threading;
using NUnit.Framework; 

namespace Sandbox.Timing
{
    [TestFixture]
    public class StopwatchTest
    {
        [Test]
        public void ElapsedTimeShouldIncrement()
        {
            Stopwatch total = new Stopwatch();
            Stopwatch sw = new Stopwatch();
            total.Start();
            sw.Start();
            sw.Stop();
            sw.Start();

            Thread.Sleep(1000);
            sw.Stop();
            Thread.Sleep(1000);
            total.Stop();

            long restared = sw.ElapsedMilliseconds;
            long all = total.ElapsedMilliseconds;

            Assert.IsTrue(Math.Abs(all - restared) < 1015);
        }


        public void DelegatesWithEqualSigs()
        {
            ForFoo(Foo2Impl);

            ForFoo2(Foo2Impl);
        }

        public void Foo2Impl(object o)
        {
            
        }
        public delegate void Foo(object o);
        public delegate void Foo2(object o);


        public void ForFoo(Foo foo)
        {

        }

        public void ForFoo2(Foo2 foo)
        {

        }


    }
}