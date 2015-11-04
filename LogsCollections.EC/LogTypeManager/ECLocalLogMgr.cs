using System.IO;
using System.Linq;

namespace LogsCollections.EC.LogTypeManager
{
    public class EcLocalLogMgr : FilesMgrBase
    {
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