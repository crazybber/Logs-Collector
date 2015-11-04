using LogsCollections.EC.Template;
using System.IO;
using System.Linq;

namespace LogsCollections.EC.LogTypeManager
{
    public class AdapterLogMgr : SandBoxLogMgr
    {
        private static readonly AdapterLogMgr Instance = SingletonProvider<AdapterLogMgr>.GetInstance();

        public new static AdapterLogMgr GetInstance()
        {
            return Instance;
        }

        public override string GetCurrentTypeLogRootDir()
        {
            var colletdir = GetNextAllLogTypeRootDirFullPath();

            var adaptordirname = Path.Combine(colletdir, "ECAdapter");

            return adaptordirname;
            // return base.GetCurrentTypeLogRootDir();
        }


  
    }
}