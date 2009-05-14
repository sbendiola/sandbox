using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Spring.Context.Support;
using Spring.Objects.Factory;
using Spring.Objects.Factory.Config;
using Spring.Util;

namespace Sandbox.Spring.Layered
{
    [TestFixture]
    public class TestLayering
    {
        [Test]
        public void ShouldLoad()
        {
            IList<IObjectFactoryPostProcessor> all = new List<IObjectFactoryPostProcessor>();
           
            XmlApplicationContext child1 = new XmlApplicationContext("file://Spring/Layered/child1.xml");
            IDictionary processors = child1.GetObjectsOfType(typeof (IObjectFactoryPostProcessor));
            IEnumerator enumerator = processors.Values.GetEnumerator();

            while(enumerator.MoveNext())
            {
                all.Add((IObjectFactoryPostProcessor)enumerator.Current);
            }
            
            XmlApplicationContext child2 = new XmlApplicationContext(false, null, true, child1, "file://Spring/Layered/child2.xml");
            foreach (IObjectFactoryPostProcessor entry in all)
            {
                child2.AddObjectFactoryPostProcessor(entry);
            }
            child2.Refresh();
            XmlApplicationContext main = new XmlApplicationContext(false, null, true, child2, "file://Spring/Layered/main.xml");

            foreach (IObjectFactoryPostProcessor entry in all)
            {
                main.AddObjectFactoryPostProcessor(entry);
            }
            main.Refresh();
            Aggregate agg = (Aggregate) main.GetObject("agg");
            Assert.IsNotNull(agg);

            Service3 three = (Service3) main.GetObject("service3");

            Assert.IsNotNull(three);

            PostProcessor processor = (PostProcessor)main.GetObject("PostProcessor");

            Assert.IsNotNull(processor);
            IList<string> names = processor.Names;
            Assert.AreEqual(3, names.Count);
            Assert.IsTrue(names.Contains("foo"));
            Assert.IsTrue(names.Contains("barService"));
            Assert.IsTrue(names.Contains("connection"));

        }
    }

    public class FakeDbConnection
    {
        private readonly string name;

        public FakeDbConnection(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get { return name; }
        }
    }

    public class Service1
    {
        private readonly FakeDbConnection connection;
        private string name;

        public Service1(FakeDbConnection connection)
        {
            this.connection = connection;
        }

        public FakeDbConnection Connection
        {
            get { return connection; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }

    public class Service2
    {
        private readonly FakeDbConnection connection;
        private string name;

        public Service2(FakeDbConnection connection)
        {
            this.connection = connection;
        }

        public FakeDbConnection Connection
        {
            get { return connection; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }

    public class Service3
    {
        private readonly FakeDbConnection connection;
        private string name;

        public Service3(FakeDbConnection connection)
        {
            this.connection = connection;
        }

        public FakeDbConnection Connection
        {
            get { return connection; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }

    public class Aggregate : IInitializingObject
    {
        private readonly Service1 one;
        private readonly Service2 two;
        private readonly Foo foo;
        private readonly Bar bar;

        public Aggregate(Service1 one, Service2 two, Foo foo, Bar bar)
        {
            this.one = one;
            this.two = two;
            this.foo = foo;
            this.bar = bar;
        }

        public Service1 One
        {
            get { return one; }
        }

        public Service2 Two
        {
            get { return two; }
        }

        public void AfterPropertiesSet()
        {
            AssertUtils.ArgumentNotNull(one, "one");
            AssertUtils.ArgumentNotNull(two, "two");
            AssertUtils.ArgumentNotNull(foo, "foo");
            AssertUtils.ArgumentNotNull(bar, "bar");
        }
    }

    public class Foo
    {
        
    }

    public class Bar
    {

    }

    public class PostProcessor : IObjectFactoryPostProcessor
    {
        private List<string> names = new List<string>();

        public List<string> Names
        {
            get { return names; }
            set { names = value; }
        }

        

        public void PostProcessObjectFactory(IConfigurableListableObjectFactory factory)
        {
            string[] lnames = factory.GetObjectDefinitionNames();
            names.AddRange(lnames);
        }
    }
}