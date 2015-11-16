
using LogsCollections.EC;
using LogsCollections.EC.LogTypeManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UnitTest2
    {
        private readonly SystemEventLogMgr _syslogmgr = SystemEventLogMgr.GetInstance();

        private readonly EcLocalLogMgr _eclocallogsmgr = EcLocalLogMgr.GetInstance();
        [TestMethod]
        public void TestMethod1()
        {
            _syslogmgr.CollectSystemLog();
        }



        [TestMethod]
        public void TestECLocalLogs()
        {
            const string husdir = @"C:\Program Files (x86)\";
            var logdirlist = LogPathSetsMgr.GetInstance(husdir).GetlogPathByType(LogType.LogEc);

            var parameter = new LogItemInfo { LogItemPaths = logdirlist };

            _eclocallogsmgr.CollectLogsFiles(parameter);
        }


    }
}
