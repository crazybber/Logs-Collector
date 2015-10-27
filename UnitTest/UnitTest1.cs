
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogsCollections.EC.LogTypeManager;

namespace UnitTest
{
    //C:\Program Files (x86)\Honeywell\HUS\EC\SandboxFramework\ECLoader\Sandbox\Logs
    [TestClass]
    public class UnitTest1
    {

        private readonly FilesMgrBase _fm = new FilesMgrBase();
        [TestMethod]
        public void TestDelAndCreate()
        {

            var curdir = _fm.GetCurrentWorkingDir();

            Trace.WriteLine(curdir);

            _fm.CreateIfNotExist("ssss");

            const string dirname = @"D:\Code\Eamon\LogsCollections.EC\UnitTest\bin\Debug\Test\logs";
            _fm.DelDirAndFilesInDir(dirname);

        }


        private static bool checkvalue(string instr, string pattern)
        {
            return Regex.IsMatch(instr, pattern, RegexOptions.IgnoreCase);
        }


        [TestMethod]
        public void TestRegular()
        {
            var instr = @"adaptr.log";


            var pattern = @"^[a-zA-Z]*\.(log|.1)$";


            var ret = checkvalue(instr, pattern);

            Assert.AreEqual(true, ret);


            instr = @"ecserver_log_";

            pattern = @"^ecserver_log_$";
            ret = checkvalue(instr, pattern);

            Assert.AreEqual(true, ret);

            instr = "100"; // 1,2,11,34,26,78,90,100.

            pattern = @"^[1-9](\d?0?)$";
            ret = checkvalue(instr, pattern);

            Assert.AreEqual(true, ret);





            instr = @"ecserver_log_.80";

            pattern = @"^ecserver_log_(\.[1-9](\d?0?))?$";
            ret = checkvalue(instr, pattern);

            Assert.AreEqual(true, ret);



            instr = @"ecserver_log_";

            pattern = @"^ecserver_log_(\.[1-9](\d?0?))?$";
            ret = checkvalue(instr, pattern);

            Assert.AreEqual(true, ret);



            pattern = @"^[a-zA-Z_]{5,20}(\.(|txt|log|\d{1,3}))?$";


            ret = checkvalue(instr, pattern);

            Assert.AreEqual(true, ret);



            instr = @"adpeter.log";

            ret = checkvalue(instr, pattern);

            Assert.AreEqual(true, ret);


            instr = @"ecserver_log_.80";

            ret = checkvalue(instr, pattern);

            Assert.AreEqual(true, ret);


            instr = @"ecserver_log_.100";

            ret = checkvalue(instr, pattern);

            Assert.AreEqual(true, ret);
        }




        [TestMethod]
        public void TestRegularFullPath()
        {
            //var instr = @"C:\Program Files (x86)\Honeywell\HUS\EC\SandboxFramework\ECLoader\Sandbox\Logs\AMTK\ADpter.log";
            // instr = @"C:\Program Files (x86)\Honeywell\HUS\EC\SandboxFramework\ECLoader\Sandbox\Logs\AMTK\";

            // var pattern = @"^[a-zA-Z]{5,20}?(\.(|txt|log|\d{1,3}))?$";

            //  var pattern = @"^(?<fpath>([a-zA-Z]:\\)(?:[\s\.\-\w]+\\)*)(?<name>[a-zA-Z]*(?<ext>\.(|txt|log|\d{1,3}))?)$";

            var instr = @"C:\";

            var pattern = @"^([a-zA-Z]:\\)$";
            var ret = checkvalue(instr, pattern);

            Assert.AreEqual(true, ret);


            instr = @"C:\Program Files (x86)\";
            pattern = @"^([a-zA-Z]:\\)(?:[\s\.\-\w\(\)]+\\)*$";
            ret = checkvalue(instr, pattern);

            Assert.AreEqual(true, ret);

            instr = @"C:\Program Files (x86)\";
            pattern = @"^([a-zA-Z]:\\)(?:[\s\.\-\w\(\)]+\\)*$";
            ret = checkvalue(instr, pattern);

            Assert.AreEqual(true, ret);


            instr = @"C:\Program Files (x86)\Honeywell\HUS\EC\SandboxFramework\ECLoader\Sandbox\Logs\AMTK\";
            pattern = @"^([a-zA-Z]:\\)(?:[\s\.\-\w\(\)]+\\)*?$";
            ret = checkvalue(instr, pattern);

            Assert.AreEqual(true, ret);


            instr = @"C:\Program Files (x86)\Honeywell\HUS\EC\SandboxFramework\ECLoader\Sandbox\Logs\AMTK\ADpter.log";
            pattern = @"^([a-zA-Z]:\\)(?:[\s\.\-\w\(\)]+\\)*?[a-zA-Z]{5,20}?(\.(|txt|log|\d{1,3}))?$";
            ret = checkvalue(instr, pattern);

            Assert.AreEqual(true, ret);


        }


        [TestMethod]
        public void TestGetDirsFiles()
        {
            const string dirPath = @"C:\Program Files (x86)\Honeywell\HUS\EC\SandboxFramework\ECLoader\Sandbox\Logs\AMTK";

            // var pattern = @"^[a-zA-Z]*\.(log|.1)$";
            const string extStringPattern = @"^[a-zA-Z]{5,20}(\.(|txt|log|\d{1,3}))?$";

            var filelist = Directory.EnumerateFiles(dirPath).ToList();

            if (filelist.Count < 1) return;

            var filenamelist = new List<string>();
            filelist.ForEach(s =>
            {

                var name = Path.GetFileName(s);

                if (name != null && Regex.IsMatch(name, extStringPattern, RegexOptions.IgnoreCase))
                {
                    filenamelist.Add(s);

                }

            });

            filenamelist.ForEach(s => Trace.WriteLine(s));

            Assert.AreNotEqual(null, filelist);
            Assert.AreEqual(1, filelist.Count);
            //Trace.WriteLine(filelist);
            //.Where(s => Regex.IsMatch(s, extStringPattern, RegexOptions.IgnoreCase)).

        }


        [TestMethod]
        public void TestGetDirsFilesLinq()
        {
            const string dirPath = @"C:\Program Files (x86)\Honeywell\HUS\EC\SandboxFramework\ECLoader\Sandbox\Logs\AMTK";

            // var pattern = @"^[a-zA-Z]*\.(log|.1)$";
            const string extStringPattern = @"^[a-zA-Z]{5,20}?(\.(|txt|log|\d{1,3}))?$";

            var filelist = Directory.EnumerateFiles(dirPath).Where(s =>
            {
                var name = Path.GetFileName(s);

                return name != null && Regex.IsMatch(name, extStringPattern, RegexOptions.IgnoreCase);

            }).ToList();





            filelist.ForEach(s => Trace.WriteLine(s));

            Assert.AreNotEqual(null, filelist);
            Assert.AreEqual(1, filelist.Count);
            //Trace.WriteLine(filelist);
            //.Where(s => Regex.IsMatch(s, extStringPattern, RegexOptions.IgnoreCase)).

        }



        [TestMethod]
        public void TestGetDirsFullFilesPathLinq()
        {
            const string dirPath = @"C:\Program Files (x86)\Honeywell\HUS\EC\SandboxFramework\ECLoader\Sandbox\Logs\AMTK";

            // var pattern = @"^[a-zA-Z]*\.(log|.1)$";
            const string extStringPattern = @"^([a-zA-Z]:\\)(?:[\s\.\-\w\(\)]+\\)*?[a-zA-Z]{5,20}?(\.(|txt|log|\d{1,3}))?$";

            var filelist = Directory.EnumerateFiles(dirPath).Where(s => Regex.IsMatch(s, extStringPattern, RegexOptions.IgnoreCase)).ToList();

            filelist.ForEach(s => Trace.WriteLine(s));

            Assert.AreNotEqual(null, filelist);
            Assert.AreEqual(1, filelist.Count);
            //Trace.WriteLine(filelist);
            //.Where(s => Regex.IsMatch(s, extStringPattern, RegexOptions.IgnoreCase)).

        }



        [TestMethod]
        public void TestGetFileName()
        {
            const string filestr = @"C:\Program Files (x86)\Honeywell\HUS\EC\SandboxFramework\ECLoader\Sandbox\Logs";

            var lastdirname = Path.GetFileName(filestr);
            Trace.WriteLine(lastdirname);


            Assert.AreEqual("Logs", lastdirname);



        }

        [TestMethod]
        public void TestMoveFiles()
        {
            const string srcdir = @"C:\Program Files (x86)\Honeywell\HUS\EC\SandboxFramework\ECLoader\Sandbox\Logs";

            const string desdir = @"D:\Code\Eamon\LogsCollections.EC\UnitTest\bin\Debug";
            const string pattern = @"^([a-zA-Z]:\\)(?:[\s\.\-\w\(\)]+\\)*?[\w\.\-]{5,20}?(\.(|txt|log|\d{1,3}))?$";
            
            _fm.CopyLogfileByDirTree(desdir, srcdir, pattern);

        }
    }
}
