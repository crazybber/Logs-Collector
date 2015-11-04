using System;
using LogsCollections.EC.LogTypeManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UnitTest2
    {
        private readonly SystemEventLogMgr _syslogmgr = SystemEventLogMgr.GetInstance();
        [TestMethod]
        public void TestMethod1()
        {
            _syslogmgr.CollectSystemLog();
        }
    }
}
