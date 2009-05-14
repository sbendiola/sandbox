using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;

namespace Sandbox.Timing
{
    [TestFixture]
    public class TimerTest
    {
        
        [Test]
        public void DisposeShouldStopInvocations()
        {
            ManualResetEvent manual = new ManualResetEvent(false);
            Counter counter = new Counter(delegate
                                              {
                                                  manual.Set();


                                              });
            Timer timer = new Timer(counter.AddOne, null, 10, 1);
            counter.Timer = timer;

            if (manual.WaitOne(1000, false) == false)
            {
                Assert.Fail();
            }

            Assert.AreEqual(5, counter.Count);
                
        }

        class Counter
        {
            private readonly Action<object> handle;
            private int count = 0;
            private Timer timer;
            public Counter(Action<object> handle)
            {
                this.handle = handle;
            }

            public int Count
            {
                get { return count; }
                set { count = value; }
            }

            public Timer Timer
            {
                set { timer = value; }
            }

            public void AddOne(object state)
            {
                Count++;
                if (count == 5)
                {
                    timer.Dispose();
                    handle(this);
                }
            }
        }

           
    }


}
