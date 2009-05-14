using System;
using NUnit.Framework;

namespace Sandbox.Runtime
{
    [TestFixture]
    public class TailCallSupport
    {
        [Test] 
        public void RecursiveCallShouldNotIncrementStackIfTailCall()
        {
            CheckTailCall();
        }

        private int count;
        private int previousStackSize = 0;

        public void CheckTailCall()
        {
            if (previousStackSize == 0)
            {
                previousStackSize = Environment.StackTrace.Length;
            }
            
            if (previousStackSize > 0 && previousStackSize < Environment.StackTrace.Length)
            {
                throw new Exception("stack size increasing was " + previousStackSize + " now " + Environment.StackTrace.Length + " count =" + count);
            }

            if (count++ < 100)
            {
                CheckTailCall();    
            }
        }
    }
}