using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using System.Linq;

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

        [Test]
        public void ShouldAddToTheFront()
        {
            var result = AddToFront(new[] {1, 2, 3}, 0);
            Assert.AreEqual(result.ToList(), new[] {0, 1, 2, 3 });
        }

        [Test]
        public void ShouldAddToTheEnd()
        {
            var result = AddToEnd(new[] { 1, 2, 3 }, 0);
            Assert.AreEqual(result.ToList(), new[] { 1, 2, 3, 0 });
        }

        private IEnumerable<int> AddToFront(IEnumerable<int> existing, int addition)
        {
            yield return addition;
            foreach (var i in existing)
            {
                yield return i;
            }
        }


        private IEnumerable<int> AddToEnd(IEnumerable<int> existing, int addition)
        {
            foreach (var i in existing)
            {
                yield return i;
            }
            yield return addition;
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
