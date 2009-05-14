using System;
using NUnit.Framework;

namespace Sandbox.Utils
{
    [TestFixture]
    public class ComparisonsTest
    {
        [Test]
        public void ShouldOnlyAllowSameType()
        {
            ComparisonBuilder builder = new ComparisonBuilder();
            if (builder.Add("foo", "bar").IsEqual())
            {
                if (builder.Add(1, 2).IsEqual())
                {
                    if (builder.Add(2d, 3d).IsEqual())
                    {

                    }   
                }
            }
            int result = builder.Result;
        }
    }


    public class ComparisonBuilder
    {
        private int result = 0;

        public ComparisonBuilder()
        {
    
        }

        public int Result
        {
            get { return result; }
        }

        public Boolean IsEqual()
        {
            return result == 0;
        }
        public ComparisonBuilder Add<T>(T first, T second) where T : IComparable<T>
        {
            if (first == null)
            {
                if (second == null) 
                {
                    
                    return this;
                } else
                {
                    result += 1;
                    return this;
                }
            }
            result += first.CompareTo(second);
            return this;
        }
    }
}