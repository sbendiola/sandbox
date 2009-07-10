using System;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;

namespace Sandbox.Linq
{
    [TestFixture]
    public class ExpressionTest
    {
        [Test]
        public void ShouldBeMemberExpression()
        {
            Expression<Func<Foo, int>> expression = x => x.IntProperty;
            LambdaExpression lambda = expression;
            Assert.AreEqual(1, lambda.Parameters.Count);
            var first = lambda.Parameters.First();
            Assert.AreEqual("x", first.Name);
            Assert.AreEqual(ExpressionType.Parameter, first.NodeType);
            Assert.AreEqual(typeof(Foo), first.Type);
            var memberExpression = (MemberExpression)lambda.Body;
            Assert.IsNotNull(memberExpression);
        }

        [Test]
        public void ShouldBeMethodCallExpression()
        {
            Expression<Func<Foo, int>> expression = x => x.Bar.IntMethod();
            LambdaExpression lambda = expression;
            Assert.AreEqual(1, lambda.Parameters.Count);
            var first = lambda.Parameters.First();
            Assert.AreEqual("x", first.Name);
            Assert.AreEqual(ExpressionType.Parameter, first.NodeType);
            Assert.AreEqual(typeof(Foo), first.Type);
            var methodCall = lambda.Body as MethodCallExpression;
            Assert.IsNotNull(methodCall);
            Assert.AreEqual(0, methodCall.Arguments.Count);
            Assert.AreEqual("IntMethod", methodCall.Method.Name);
            var actual = methodCall.Object;
            var memberExpression = actual as MemberExpression;
            Assert.IsNotNull(memberExpression);
            Assert.AreEqual("Bar", memberExpression.Member.Name);
            
        }

        public class Foo
        {
            public int IntProperty { get; set; }
            public int IntMethod()
            {
                return 0;
            }

            public Bar Bar { get; set;}
        }

        public class Bar
        {
            public int IntProperty { get; set; }
            public int IntMethod()
            {
                return 0;
            }
        }
    }
}
