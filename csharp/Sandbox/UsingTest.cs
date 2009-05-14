using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;

namespace Sandbox
{
    [TestFixture]
    public class UsingTest
    {
        [Test]
        public void ShouldAllowNull()
        {
            using (null);
        }

        [Test]
        public void ShouldCallDispose()
        {
            MockRepository mocks = new MockRepository();
            IDisposable mock = mocks.CreateMock<IDisposable>();
            mock.Dispose();
            mocks.ReplayAll();
            using (mock)
            {

            }
        }
    }
}
