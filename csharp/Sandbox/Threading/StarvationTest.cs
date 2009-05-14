using System;
using System.Diagnostics;
using System.Timers;
using NUnit.Framework;

namespace Sandbox.Threading
{
    [TestFixture]
    public class StarvationTest
    {
        private int count = 0;

        [Test]
        public void MainthreadCanStopOtherThreads()
        {
            Timer t = new Timer(100);
            t.Elapsed += new ElapsedEventHandler(t_Elapsed);
            t.Start();
            int local = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds < 5000)
            {
                local++;
            }
            int expected = 5000/100;
            Assert.IsTrue(count > expected, "count was " + count + " should be at least" + expected + " local was " + local);
        }

        void t_Elapsed(object sender, ElapsedEventArgs e)
        {
            count++;
        }



        
    }
}