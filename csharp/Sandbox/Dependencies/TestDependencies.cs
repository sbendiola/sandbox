using System;
using System.Collections.Generic;
using System.Reflection;

using System.Text;
using NUnit.Framework;
using Sandbox.Dependencies.MyNamespace;
using System.Linq;
using Sandbox.Dependencies.MyNamespaceFoo;

namespace Sandbox.Dependencies
{
    namespace MyNamespaceFoo
    {
        public class Common
        {

        }

        public class Returned
        {

        }

        public class ActionArg
        {

        }    
    }
    

    namespace MyNamespace
    {
        public class Specific2
        {
            
        }
        public class Specific
        {
            public Returned MethodThatDependsOnCommon<T>(Action<T> action) where T : ActionArg
            {
                var s2 = new Specific2();
                var s = new StringBuilder();
                Action<ActionArg> z = null;
                    
                try
                {
                    var x = new MyNamespaceFoo.Common();    
                    
                } catch(Exception e)
                {
                    
                }

                return null;


            }
        }
    }

    [TestFixture]
    public class TestDependencies
    {
        [Test]
        public void ShouldBeAbletoGetClassesFromAssembly()
        {
            var type = typeof (Action<String>);
            Example.Main();
            Example.MethodDependencies(typeof (Specific).GetMethod("MethodThatDependsOnCommon")).ToList().ForEach(Console.WriteLine);
//            Console.WriteLine(typeof (Specific).Namespace);
//            //FileSystemResource resource = new FileSystemResource("file://Sandbox.dll");
//            //Assert.AreEqual(true, resource.Exists);
//
//            Assembly assembly = Assembly.GetAssembly(typeof(Specific));
//            Type[] types = assembly.GetTypes();
//            Console.WriteLine("foo bar");
//            var match = typeof(Specific);
//            foreach (Type type in types)
//            {
//                
//                try
//                {
//                    if (type.Namespace != null && type.Equals(match))
//                    {
//                            Console.WriteLine(type.Name);
//                            MethodInfo[] methods = type.GetMethods();
//                            if (methods != null)
//                            {
//
//                                foreach (MethodInfo info in methods)
//                                {
//                                    try {
//                                        MethodBody body = info.GetMethodBody();
//                                        if (body != null)
//                                        {
//                                            byte[] buffer = body.GetILAsByteArray();
//
//                                            if (buffer != null)
//                                            {
//                                                string il = Encoding.ASCII.GetString(buffer);
//                                                if (il != null)
//                                                {
//                                                    
//                                                    Console.WriteLine("method -" + info.Name +  "[ "  + il + " ]");
//                                                }
//                                        
//                                            }
//                                        }
//
//                                    }
//                                    catch (Exception e)
//                                    {
//                                        throw new Exception("failed on method " + info.Name, e);
//                                    }
//                                }
//                            }
//                        
//                        
//                    }
//                }
//                catch (Exception e)
//                {
//                    throw new Exception("failed on type " + type, e);
//                }
//                
//            }
        }
    }

   
    public static class Functionals
    {
        public static R FoldLeft<T, R>(this IEnumerable<T> items, R seed, Func<R, T, R> function)
        {
            return items.Aggregate(seed, function);
        }

        private static IEnumerable<Type> CollectTypes(Type type, List<Type> types)
        {
            if (type != null)
            {
                if (types.Contains(type) == false)
                {
                    types.Add(type);

                    if (type.IsGenericType)
                    {
                        return CollectTypes(type.BaseType, types);
                    } 
                }
            }
            return types;
        }

        public static IEnumerable<Type> ExtractTypes(this Type type)
        {
            var types = new List<Type>();
            return CollectTypes(type, types);            
        }
    }

    public class Example
    {

        public static IList<Type> MethodDependencies(MethodBase method)
        {
            var body = method.GetMethodBody();
            var dependencies = new List<Type>();
            ParamaterTypes(method, dependencies);
            var genericArgs = method.GetGenericArguments();
            LocalVariableInfo info = null;
            Type type = info.LocalType;
            LocalVariableTypes(body, dependencies);
            ExceptionTypes(body, dependencies);
            return dependencies;

        }

        private static List<Type> LocalVariableTypes(MethodBody body, List<Type> dependencies)
        {
            return body.LocalVariables
                .FoldLeft(dependencies, (classes, localVariable) =>
                                            {
                                                classes.AddRange(localVariable.LocalType.ExtractTypes());
                                                return classes;
                                            });
        }

        private static List<Type> ExceptionTypes(MethodBody body, List<Type> dependencies)
        {
            return body.ExceptionHandlingClauses
                .FoldLeft(dependencies, (classes, exceptionClause) =>
                                            {
                                                classes.AddRange(exceptionClause.CatchType.ExtractTypes());
                                                return classes;
                                            });
        }

        private static List<Type> ParamaterTypes(MethodBase method, List<Type> dependencies)
        {
            return method.GetParameters()
                .FoldLeft(dependencies, (classes, paramInfo) =>
                                            {
                                                var modifiers = paramInfo.GetOptionalCustomModifiers();
                                                var attributes = paramInfo.GetCustomAttributes(false);
                                                classes.AddRange(paramInfo.ParameterType.ExtractTypes());
                                                return classes;
                                            });
        }

        
        public static void Main()
        {
            // Get method body information.
            MethodInfo mi = typeof(Specific).GetMethod("MethodThatDependsOnCommon");
            MethodBody mb = mi.GetMethodBody();
            Console.WriteLine("\r\nMethod: {0}", mi);

            // Display the general information included in the 
            // MethodBody object.
            Console.WriteLine("    Local variables are initialized: {0}", 
                mb.InitLocals);
            Console.WriteLine("    Maximum number of items on the operand stack: {0}", 
                mb.MaxStackSize);

            // Display information about the local variables in the
            // method body.
            Console.WriteLine();
            foreach (LocalVariableInfo lvi in mb.LocalVariables)
            {
                Console.WriteLine("Local variable: {0}", lvi);
            }

            // Display exception handling clauses.
            Console.WriteLine();
            foreach (ExceptionHandlingClause ehc in mb.ExceptionHandlingClauses)
            {
                Console.WriteLine(ehc.Flags.ToString());

                // The FilterOffset property is meaningful only for Filter
                // clauses. The CatchType property is not meaningful for 
                // Filter or Finally clauses. 
                switch (ehc.Flags)
                {
                    case ExceptionHandlingClauseOptions.Filter:
                        Console.WriteLine("        Filter Offset: {0}", 
                            ehc.FilterOffset);
                        break;
                    case ExceptionHandlingClauseOptions.Finally:
                        break;
                    default:
                        Console.WriteLine("    Type of exception: {0}", 
                            ehc.CatchType);
                        break;
                }

                Console.WriteLine("       Handler Length: {0}", ehc.HandlerLength);
                Console.WriteLine("       Handler Offset: {0}", ehc.HandlerOffset);
                Console.WriteLine("     Try Block Length: {0}", ehc.TryLength);
                Console.WriteLine("     Try Block Offset: {0}", ehc.TryOffset);
            }
        }

        // The Main method contains code to analyze this method, using
        // the properties and methods of the MethodBody class.
        public void MethodBodyExample(object arg)
        {
            // Define some local variables. In addition to these variables,
            // the local variable list includes the variables scoped to 
            // the catch clauses.
            int var1 = 42;
            string var2 = "Forty-two";

            try
            {
                // Depending on the input value, throw an ArgumentException or 
                // an ArgumentNullException to test the Catch clauses.
                if (arg == null)
                {
                    throw new ArgumentNullException("The argument cannot be null.");
                }
                if (arg.GetType() == typeof(string))
                {
                    throw new ArgumentException("The argument cannot be a string.");
                }        
            }

            // There is no Filter clause in this code example. See the Visual 
            // Basic code for an example of a Filter clause.

            // This catch clause handles the ArgumentException class, and
            // any other class derived from Exception.
            catch(Exception ex)
            {
                Console.WriteLine("Ordinary exception-handling clause caught: {0}", 
                    ex.GetType());
            }        
            finally
            {
                var1 = 3033;
                var2 = "Another string.";
            }
        }
    }

}
