using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DefaultNamespace
{
    [TestFixture]
    public class NunitLifecycleTest
    {
        private Foo foo = new Foo();
        private bool throwException = false;
        private Exception e;
        private int run = 0;
        private int testfixturesetup = 0;
        [SetUp]
        public void SetUp()
        {
            try
            {
                if (run == 0)
                {
                    throw new Exception();
                }                
            } finally
            {
                run++;
            }

            
        }

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            testfixturesetup++;

        }

        [TearDown]
        public void TearDown()
        {
            Assert.AreEqual(1, foo.count);
        }

        [Test]
        public void ThrowExceptionInSetup()
        {
            Assert.AreEqual(1, foo.count);
        }

        [Test]
        public void OneTestMethod()
        {
            Assert.AreEqual(1, foo.count);
        }

        [Test]
        public void SecondTestMethod()
        {
            Assert.AreEqual(1, foo.count);
        }

        public void TestRunnerLooksForMethodsNamedTest()
        {
            Assert.Fail("this should fail");
        }

        class Foo
        {
            public int count = 0;
            public Foo()
            {
                count++;
            }
        }
    }
}