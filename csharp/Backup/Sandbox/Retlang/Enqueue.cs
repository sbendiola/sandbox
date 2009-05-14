using System.Threading;
using NUnit.Framework;
using Retlang;

namespace Sandbox.Retlang
{
    [TestFixture]
    public class Enqueue
    {
        private int count = 0;

        [Test]
        public void ScheduleOnInterval_ShouldBlockSubsequentCalls()
        {
            ProcessContextFactory factory = new ProcessContextFactory();
            factory.Start();
            IProcessContext fooProcess = factory.CreateAndStart("foo");
            fooProcess.ScheduleOnInterval(Sleeps, 0, 10);

            Thread.Sleep(1000);
            Assert.AreEqual(1, count);
        }

        [Test]
        public void ScheduleOnInterval_ShouldRepeatsCalls()
        {
            ProcessContextFactory factory = new ProcessContextFactory();
            factory.Start();
            IProcessContext fooProcess = factory.CreateAndStart("foo");
            fooProcess.ScheduleOnInterval(Normal, 0, 10);
            Thread.Sleep(1000);
            Assert.IsTrue(count > 1);
        }

        [Test]
        public void CreatePooled_ShouldBlockSubsequentCalls()
        {
            ProcessContextFactory factory = new ProcessContextFactory();
            factory.Start();
            IProcessBus pooled = factory.CreatePooled();
            object topic = pooled.CreateUniqueTopic();

            Thread.Sleep(1000);
            Assert.AreEqual(1, count);
        }


        [SetUp]
        public void Setup()
        {
            count = 0;
        }

        void Normal()
        {
            count++;
        }
        
        void Sleeps()
        {
            count++;
            Thread.Sleep(1000);
        }
    }
}