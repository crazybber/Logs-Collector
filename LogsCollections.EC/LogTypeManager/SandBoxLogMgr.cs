using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogsCollections.EC.Template;

namespace LogsCollections.EC.LogTypeManager
{
    class SandBoxLogMgr : FilesMgrBase
    {
        private static readonly SandBoxLogMgr Instance = SingletonProvider<SandBoxLogMgr>.GetInstance();



        public static SandBoxLogMgr GetInstance()
        {
            return Instance;
        }

        public void CollectLogsFiles(LogItemInfo  loginfo)
        {
            


        }


    }
}
