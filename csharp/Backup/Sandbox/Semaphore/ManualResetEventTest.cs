using System.Threading;
using NUnit.Framework;

namespace Sandbox.Semaphore
{
    [TestFixture]
    public class ManualResetEventTest
    {
        
        [Test]
        public void ShouldAllowMultipleSets()
        {

            ManualResetEvent manualResetEvent = new ManualResetEvent(false);
            manualResetEvent.Set();
            manualResetEvent.Set();
        }
    }
}