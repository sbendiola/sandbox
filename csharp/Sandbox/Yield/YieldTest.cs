using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;

namespace Sandbox.Yield
{
    [TestFixture]
    public class YieldTest
    {
        private Stopwatch sw = new Stopwatch();
        [Test]
        public void ShouldBeLazy()
        {
            sw.Start();
            IList<int> results = new List<int>();
            
            IEnumerator<int> list = BuildList();
            Console.WriteLine(sw.ElapsedMilliseconds);
            while(results.Count < 5)
            {
                Assert.IsTrue(list.MoveNext());
                results.Add(list.Current);
            }
            sw.Stop();
        }

        public IEnumerator<int> BuildList()
        {
            int count = 0;
            while(true)
            {
                int newcount = count++;
                Console.WriteLine("returning " + newcount +  " " + sw.ElapsedMilliseconds);
                yield return newcount;
            }
        }
    }
}
