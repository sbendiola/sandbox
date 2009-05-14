using NUnit.Framework;

namespace Sandbox.Spec.implicits
{

    [TestFixture]
    public class ImplicitConversion
    {
        public class Foo
        {
            public int Bar()
            {
                return 0;
            }

            public static implicit operator int(Foo d)
            {
                return d.Bar();
            }

            public static implicit operator Foo(int d)
            {
                return new Foo();
            }
        }
       
        [Test]
        public void CompilerDoesNotAutomaticallyConvert()
        {
            int x = 0;
            Foo f = x;
            f.Bar();
        }

    }

}