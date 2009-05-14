using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using NUnit.Framework;
namespace Sandbox
{
    [TestFixture]
    public class ReflectionTest
    {
        [Test]
        public void ShouldGetParamNamesFromMethod()
        {
            MethodInfo method = this.GetType().GetMethod("MethodWithTwoArguments");
            ParameterInfo[] parameters = method.GetParameters();
            List<string> expected = new List<string>(new string[] {"str", "anint"});
            Assert.AreEqual(2, parameters.Length);

            foreach (ParameterInfo info in parameters)
            {
                expected.Remove(info.Name);
            }
            Assert.AreEqual(0, expected.Count);
        }

        public void MethodWithTwoArguments(String str, int anint)
        {
            
        }
    }
}
