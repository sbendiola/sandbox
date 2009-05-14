using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;
using Rhino.Mocks;

namespace Sandbox.Collections
{
    [TestFixture]
    public class ListTest
    {
        [Test]
        
        public void ForEachShouldTerminateOnException()
        {
            MockRepository mocks = new MockRepository();

            IDisposable d1 = mocks.CreateMock<IDisposable>();
            IDisposable d2 = mocks.CreateMock<IDisposable>();
            List<IDisposable> list = new List<IDisposable>();
            list.Add(d1);
            list.Add(d2);
            using(mocks.Record())
            {
                d1.Dispose();
                LastCall.Throw(new ArgumentException());
            }
            
            mocks.ReplayAll();
            try
            {
                list.ForEach(delegate(IDisposable it) { it.Dispose(); });    
            } catch(ArgumentException expected)
            {
                
            }    
        }

        [Test]
        public void ShouldBebleToRemoveNullFromList()
        {
            List<string> list = new List<string>();
            list.Remove(null);
            list.Add("serw");
            list.Remove(null);
        }


        [Test]
        public void IterationThroughListShouldNotCauseConcurrentModification()
        {
            List<int> strings = new List<int>();
            strings.Add(strings.Count);
            strings.Add(strings.Count);


            Boolean b = false;
            

            new Thread(delegate()
                           {
                               while (strings.Count < 100) {
                                   foreach (int s in strings)
                                   {
                                       strings.Add(strings.Count);
                                       Thread.Sleep(5);
                                   }
/*                                   strings.ForEach(delegate(int it)
                                                       {
                                                           strings.Add(strings.Count);
                                                           Thread.Sleep(5);
                                                       });*/

                                   Console.WriteLine("strings.Count " + strings.Count);
                               } 
                               b = true;
                           }).Start();

            strings.ForEach(delegate(int it)
            {
                if (b == false)
                {
                    foreach (int s in strings)
                    {
                        strings.Add(strings.Count);
                        Thread.Sleep(100);
                    }   
                }
            });
        }
    }
}
