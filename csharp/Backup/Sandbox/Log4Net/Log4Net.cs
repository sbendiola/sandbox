using System;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Repository;
using log4net.Repository.Hierarchy;
using NUnit.Framework;

namespace Sandbox.Log4Net
{
    [TestFixture]
    public class Log4Net
    {

        
        
        [Test]
        public void DuplicateLogEntriesShouldNotStopLogging()
        {
            XmlConfigurator.Configure(new System.IO.FileInfo("log4net.config.xml"));
            ILog foobar = LogManager.GetLogger("foo.bar");
            ILog wtf = LogManager.GetLogger("wtf");
            Assert.AreEqual(true, foobar.IsInfoEnabled);
            Assert.AreEqual(true, wtf.IsDebugEnabled);

            XmlConfigurator.Configure(new System.IO.FileInfo("dup.log4net.config.xml"));

            Assert.AreEqual(true, LogManager.GetLogger("debug.debug").IsDebugEnabled);
            Assert.AreEqual(true, LogManager.GetLogger("info.debug").IsDebugEnabled);
            Assert.AreEqual(true, LogManager.GetLogger("WARN.debug").IsDebugEnabled);
            
        }

    }
}