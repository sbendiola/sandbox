using NUnit.Framework;

namespace Sandbox.DateTime
{
    [TestFixture]
    public class TicksTest
    {
        [Test]
        public void ShouldBeTenThousandTicksPerMillisecond()
        {
            System.DateTime now = System.DateTime.Now;
            System.DateTime later = now.AddMilliseconds(1);
            Assert.AreEqual(now.Ticks + 10000, later.Ticks);
        }
    }
}