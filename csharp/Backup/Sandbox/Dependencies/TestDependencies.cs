using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using Spring.Core.IO;

namespace Sandbox.Dependencies
{
    [TestFixture]
    public class TestDependencies
    {
        [Test]
        public void ShouldBeAbletoGetClassesFromAssembly()
        {
            FileSystemResource resource = new FileSystemResource("file://Sandbox.dll");
            Assert.AreEqual(true, resource.Exists);

            Assembly assembly = Assembly.LoadFile(resource.File.FullName);
            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                MethodInfo[] methods = type.GetMethods();
                if (methods != null)
                {
                    foreach (MethodInfo info in methods)
                    {
                        MethodBody body = info.GetMethodBody();
                        if (body != null)
                        {
                            byte[] buffer = body.GetILAsByteArray();

                            if (buffer != null)
                            {
                                string il = System.Text.Encoding.ASCII.GetString(buffer);
                                Console.WriteLine(il);
                            }    
                        }
                    }    
                }
                
                Console.WriteLine(type.Name);
            }
        }
    }
}
