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
            var mock = MockRepository.GenerateMock<IDisposable>();            
            using (mock)
            {

            }
            mock.AssertWasCalled(m => m.Dispose());
        }

        [Test]
        public void ShouldCallDisposeWhenExceptionThrown()
        {
            var mock = MockRepository.GenerateMock<IDisposable>();
            try
            {
                using (mock)
                {
                    throw new Exception();
                }    
                Assert.Fail();
            } catch(Exception expected)
            {
                mock.AssertWasCalled(m => m.Dispose());    
            }
            
        }
    }
}
