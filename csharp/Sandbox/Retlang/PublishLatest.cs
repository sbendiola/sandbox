using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Retlang;
using Rhino.Mocks;
using Is=Rhino.Mocks.Constraints.Is;

namespace Sandbox.Retlang
{
    [TestFixture]
    public class PublishLatest
    {
        public void ShouldPublish()
        {
            ProcessContextFactory factory = new ProcessContextFactory();
            factory.Start();
            IProcessContext foo = factory.CreateAndStart("foo");
            TopicEquals topic = new TopicEquals("foo.bar");
            foo.SubscribeToLast<Int32>(topic, OnFoo, 1000);
            
            int count = 0;
            while( count++ < 5000000)
            {
                foo.Publish("foo.bar", count);
            }
            Thread.Sleep(10000);    

            
            
        }
        private List<Int32> received = new List<Int32>();

        private void OnFoo(IMessageHeader header, Int32 msg)
        {
            received.Add(msg);
        }

        [Test]
        public void PublishToSameProcessContext()
        {
            MockRepository repository = new MockRepository();

            ProcessContextFactory factory = new ProcessContextFactory();
            factory.Start();
            IProcessContext processContext = factory.CreateAndStart();
            object o = new object();

            OnMessage<object> action = repository.CreateMock<OnMessage<object>>();
            processContext.Subscribe(new TopicEquals(o), action);

            using(repository.Record())
            {
                action(null, o);
                LastCall.Constraints(Is.NotNull(), Is.NotNull());
            }

            using (repository.Playback())
            {
                processContext.Publish(o, o);
                Thread.Sleep(100);
            }
                
        }

        [Test]
        public void PublishToProcessContextInSameProcessContextFactory()
        {
            MockRepository repository = new MockRepository();

            ProcessContextFactory factory = new ProcessContextFactory();
            factory.Start();
            IProcessContext another = factory.CreateAndStart();
            IProcessContext processContext = factory.CreateAndStart();
            object o = new object();

            OnMessage<object> action = repository.CreateMock<OnMessage<object>>();
            another.Subscribe(new TopicEquals(o), action);

            using (repository.Record())
            {
                action(null, o);
                LastCall.Constraints(Is.NotNull(), Is.NotNull());
            }

            using (repository.Playback())
            {
                processContext.Publish(o, o);
                Thread.Sleep(100);
            }

        }

        [Test]
        public void PublishReentrantToProcessContextInSameProcessContextFactory()
        {
            
            ProcessContextFactory factory = new ProcessContextFactory();
            factory.Start();

            IProcessContext another = factory.CreateAndStart(new CustomCommandExecutor());
            IProcessContext processContext = factory.CreateAndStart(new CustomCommandExecutor());
            object o = "message1";
            object a = "message2";

            List<object> anothers = new List<object>();
            List<object> processContexts = new List<object>();
            another.Subscribe<object>(new TopicEquals(o), delegate(IMessageHeader obj, object msg)
                                        {

                                            if (msg == o)
                                            {
                                                another.Publish(a, o);
                                                processContext.Publish(a, o);
                                            }
                                            anothers.Add(msg);
                                        });

        
            
            processContext.Subscribe<object>(new TopicEquals(a), delegate(IMessageHeader obj, object msg)
                                        {
                                            if (msg == a)
                                            {
                                                another.Publish(o, a);
                                                processContext.Publish(a, o);
                                            }
                                            processContexts.Add(msg);
                                        });

            processContext.Publish(o, o);                               
            Thread.Sleep(1000);
            

        }

        public class CustomCommandExecutor : ICommandExecutor
        {
            public void ExecuteAll(Command[] toExecute)
            {
                foreach (Command command in toExecute)
                {
                    command();
                }
            }
        }
    }
}
