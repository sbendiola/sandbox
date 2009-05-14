using System;
using NUnit.Framework;

namespace Sandbox.Spec
{
    [TestFixture]
    public class WhatTheNull
    {
        [Test]
        public void LocalVariableAssigntoNull()
        {
            string foo = null;
            if (Environment.TickCount == 0)
            {
                foo = "123";
            }

            Assert.IsNull(foo);
        }

        [Test]
        public void NullableFieldNeverANullPointer()
        {
            decimal? foo = null;
            Assert.IsNull(foo);
        }

        [Test]
        public void NullableAlwaysCanCallHasValue()
        {
            decimal? foo = null;
            Assert.AreEqual(false, foo.HasValue);
        }

        [Test]
        public void CompilerKnowsSemantics()
        {
            decimal? foo = null;
            String bar = null;
            if (Environment.TickCount > 0)
            {
                foo = 4;    
                bar = "123";
            }
            if (foo.HasValue)
            {
                if (bar != null)
                {

                }    
            }
            
        }

    }
}