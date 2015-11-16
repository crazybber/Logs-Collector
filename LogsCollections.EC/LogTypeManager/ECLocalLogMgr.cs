using System.IO;
using System.Linq;
using LogsCollections.EC.Template;

namespace LogsCollections.EC.LogTypeManager
{
    public class EcLocalLogMgr : FilesMgrBase
    {

        private static readonly EcLocalLogMgr Instance = SingletonProvider<EcLocalLogMgr>.GetInstance();
        //@"^([a-zA-Z]:\\)(?:[\s\.\-\w\(\)]+\\)*?[\w\.\-]{5,20}?(\.(|txt|log|\d{1,3}))?$";


        public static EcLocalLogMgr GetInstance()
        {
            return Instance;
        }

        public virtual void CollectLogsFiles(LogItemInfo loginfo)
        {
            //STEP 1 find log location

            var curLogTypeDir = GetCurrentTypeLogRootDir();

            //Create dir
            CreateIfNotExist(curLogTypeDir);

            // STEP 3 zip the src dir and file

            loginfo.LogItemPaths.ToList().ForEach(folderitem =>
            {
                CollectLogdir(curLogTypeDir, folderitem);
            });
        }

        private void CollectLogdir(string desdir, string srcpathdir)
        {
            FileCollectZipMgr.Current.zipdir_direct(desdir, srcpathdir);
        }

        public override string GetCurrentTypeLogRootDir()
        {
            var colletdir = GetNextAllLogTypeRootDirFullPath();

            var eclocaldirname = Path.Combine(colletdir, "EClocal");

            return eclocaldirname;
            // return base.GetCurrentTypeLogRootDir();
        }
    }
}