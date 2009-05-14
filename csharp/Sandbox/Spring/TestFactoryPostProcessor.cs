using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using NUnit.Framework;
using Retlang;


namespace Sandbox.Spring
{
    [TestFixture]
    public class TestThrowingException
    {
        private static ILog logger = LogManager.GetLogger(typeof (TestThrowingException));

        [Test]
        public void ShouldNotDie()
        {
            XmlConfigurator.Configure(new FileInfo("log4j.xml"));

               
            ProcessContextFactory pcf = new ProcessContextFactory();
            pcf.Start();

            List<object> work = new List<object>();

            
            IProcessContext processContext = pcf.CreateAndStart("foo bar");

            Command good = delegate
                               {
                                   int threadId = Thread.CurrentThread.GetHashCode();
                                   logger.Info("executing " + threadId);
                                   work.Add(this);
                               };
            Command bad = delegate
                              {
                                  int threadId = Thread.CurrentThread.GetHashCode();
                                  logger.Info("throwing " + threadId);
                                  throw new Exception();
                              };

            processContext.Enqueue(good);
            processContext.Enqueue(bad);
            processContext.Enqueue(good);
            processContext.Enqueue(bad);
            processContext.Join();
            Assert.AreEqual(1, work.Count);
        }
    }

    internal class LoggingCommandExecutor : ICommandExecutor
    {
        private readonly ICommandExecutor real = new CommandExecutor();
        
        public void ExecuteAll(Command[] toExecute)
        {

            real.ExecuteAll(toExecute);    
            
        }
    }
}
