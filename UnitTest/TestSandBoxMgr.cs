
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using LogsCollections.EC;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogsCollections.EC.LogTypeManager;

namespace UnitTest
{
    [TestClass]
    public class TestSandBoxMgr
    {

        private readonly SandBoxLogMgr _sabdBoxLogMgr = new SandBoxLogMgr();
        [TestMethod]
        public void TestSandLogsMgr()
        {
            const string husdir = @"C:\Program Files (x86)\Honeywell\";
            var logdirlist = LogPathSetsMgr.GetInstance(husdir).GetlogPathByType(LogType.LogSandBox);

            var parameter = new LogItemInfo { LogItemPaths = logdirlist };


            _sabdBoxLogMgr.CollectLogsFiles(parameter);


        }

        [TestMethod]
        public void TestIsFileTypeExit()
        {
            var srcdir = "C:\\Program Files (x86)\\Honeywell\\HUS\\EC\\SandboxFramework\\ECLoader\\Sandbox\\Logs";
            var pattern = @"^([a-zA-Z]:\\)(?:[\s\.\-\w\(\)]+\\)*?[\w\.\-]{5,20}?(\.(|log|txt|\d{1,3}))?$";

            var fb = new FilesMgrBase();

            var ret = fb.IsFileTypeExit(srcdir, pattern);


            Assert.IsNull(ret);
        }

        //^([a-zA-Z]:\\\\)(?:[\\s\\.\\-\\w\\(\\)]+\\\\)*?[\\w\\.\\-]{5,20}?(\\.(|log|txt|bak|\\d{1,3}))?$

        [TestMethod]
        public void TestEnumFiles()
        {

            var dirPath = "C:\\Program Files (x86)\\Honeywell\\HUS\\EC\\SandboxFramework\\ECLoader\\Sandbox\\Logs";
            var extStringPattern = @"^([a-zA-Z]:\\)(?:[\s\.\-\w\(\)]+\\)*?[\w\.\-]{5,20}?(\.(|log|txt|\d{1,3}))?$";

            var filelist = Directory.EnumerateFiles(dirPath).Where(s => Regex.IsMatch(s, extStringPattern, RegexOptions.IgnoreCase));

            var ret = filelist.ToList();

            Assert.AreEqual(ret, 0);
        }

        [TestMethod]
        public void TestGetLastDirname()
        {
            const string instr = @"C:\abc\xyz\adf\name";
            const string instr2 = @"C:\abc\xyz\adf\name\";

            var ret = Path.GetFileName(instr);

            Assert.AreEqual("name", ret);

            ret = Path.GetDirectoryName(instr2);

            ret = Path.GetFileName(ret);
            Assert.AreEqual("name", ret);
        }
    }
}
