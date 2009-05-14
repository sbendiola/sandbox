using NUnit.Framework;

namespace Sandbox.Spec
{
    [TestFixture]
    public class Statics
    {
        private static int i = 0;
        //this is a static block
        static Statics()
        {
            Assert.AreEqual(0, i);
            i++;
        }

        public Statics()
        {
            Assert.AreEqual(1, i);
        }

        [Test]
        public void StaticInitializationHappensBeforeInstanceCOnstructor()
        {
            new Statics();
        }
    }
}