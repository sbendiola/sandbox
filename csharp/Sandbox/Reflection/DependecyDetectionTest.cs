using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using NUnit.Core;
using NUnit.Framework;

namespace Sandbox.Reflection
{
    [TestFixture]
    public class DependecyDetectionTest
    {
        [Test] 
        public void GivenAnInstanceShouldBeAbleToDetermineRuntimeDependencies()
        {
            Type atype = typeof(A);
            StructLayoutAttribute attribute = atype.StructLayoutAttribute;
            MethodBase method = atype.GetMethod("MethodUsingB",new Type[0]);
            Assembly assembly = null;
        }
    }

    class A
    {
        private B a;

        public void MethodUsingB()
        {
            B b = new B();
        }
    }

    class B
    {
        
    }
}