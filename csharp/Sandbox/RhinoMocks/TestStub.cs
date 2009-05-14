using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;

namespace Sandbox.RhinoMocks
{
    [TestFixture]
    public class TestStub
    {
        [Test]
        public void CanSetExpectationsOnStub()
        {
            MockRepository mockery = new MockRepository();
            Foo stub = mockery.Stub<Foo>();
            using(mockery.Record())
            {
                Expect.Call(stub.Baz()).Return(23);
            }
            mockery.Playback();
            try
            {
                mockery.VerifyAll();
                Assert.Fail("should have throw an exception");
            } catch(Exception expected)
            {
                
            }
        }

        [Test]
        public void StubReturnsDefault()
        {
            MockRepository mockery = new MockRepository();
            Foo stub = mockery.Stub<Foo>();
            using (mockery.Record())
            {

            }
            mockery.Playback();

            int result = stub.Baz();
            Assert.AreEqual(0, result);

        }

        public interface  Foo
        {
            void Bar();
            int Baz();
        }
    }
}
