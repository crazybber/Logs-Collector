
using System.IO;
using System.Linq;
using LogsCollections.EC.Template;

namespace LogsCollections.EC.LogTypeManager
{
    public class SandBoxLogMgr : FilesMgrBase
    {
        private static readonly SandBoxLogMgr Instance = SingletonProvider<SandBoxLogMgr>.GetInstance();
        //@"^([a-zA-Z]:\\)(?:[\s\.\-\w\(\)]+\\)*?[\w\.\-]{5,20}?(\.(|txt|log|\d{1,3}))?$";
        protected const string Pattern1 = @"^([a-zA-Z]:\\)(?:[\s\.\-\w\(\)]+\\)*?[\w\.\-]{5,20}?(\.(";

        protected const string Pattern2 = @"\d{1,3}))?$";

        public static  SandBoxLogMgr GetInstance()
        {
            return Instance;
        }


        public override string GetCurrentTypeLogRootDir()
        {
            var colletdir = GetNextAllLogTypeRootDirFullPath();

            var sandboxdirname = Path.Combine(colletdir, "SandBoxLog");

            return sandboxdirname;

        }

        public virtual void CollectLogsFiles(LogItemInfo loginfo)
        {
            //STEP 1 find log location 
            var sandboxlogroot = loginfo.LogItemPaths.First();

            var curLogTypeDir = GetCurrentTypeLogRootDir();

            var pattern = GetFileExtStr();
            //Step 2 copy log file for sandbox dir to woriking dir
            CopyLogfileByDirTree(curLogTypeDir, sandboxlogroot, pattern);

            // STEP 3 zip them 

            FileCollectZipMgr.CollectFilesAndZipThem(curLogTypeDir);

            //step 4 delete

        }

        public override string GetFileExtStr()
        {
            var ret = string.Empty;

            // TODO.....


            if (ret == string.Empty)
            {
                ret = @"|log|txt|";
            }

            ret = Pattern1 + ret + Pattern2;

            return ret;

            // return base.GetFileExtStr();
        }
    }
}
