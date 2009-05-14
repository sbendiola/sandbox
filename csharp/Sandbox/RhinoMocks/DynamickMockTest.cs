using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Exceptions;

namespace Sandbox.RhinoMocks
{
    [TestFixture]
    public class DynamickMockTest
    {
        [Test]
        public void DynamicMockShouldRespectExpectations()
        {

            MockRepository mockery = new MockRepository();
            IList<string> list = mockery.DynamicMock<IList<string>>();

            using(mockery.Record())
            {
                list.Add("1");
                list.Add("foo");
                LastCall.Repeat.AtLeastOnce();
            }
            mockery.ReplayAll();
            list.Add("1");
            try
            {
                mockery.VerifyAll();  
                Assert.Fail("should have thrown an error");
            } catch(Exception expected)
            {

            }
            
        }

        [Test]
        public void VerifyAllMustBeCalledAfterRecord()
        {

            MockRepository mockery = new MockRepository();
            IList<string> list = mockery.DynamicMock<IList<string>>();

            using (mockery.Record())
            {
                list.Add("1");
            }
            
         //   list.Add("1");
         //   mockery.VerifyAll();
        }

        [Test]
        public void PlaybackInUsingShouldCallVerifyAll()
        {

            MockRepository mockery = new MockRepository();
            IList<string> list = mockery.DynamicMock<IList<string>>();


            using (mockery.Record())
            {
                list.Add("1");
            }

            try
            {
                using (mockery.Playback())
                {


                }
                Assert.Fail();
            } catch(ExpectationViolationException expected)
            {
                
            }
            

            //   list.Add("1");
            //   mockery.VerifyAll();
        }

    }
}