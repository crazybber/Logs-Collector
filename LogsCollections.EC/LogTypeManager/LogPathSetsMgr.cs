using System;
using System.Collections.Generic;
using LogsCollections.EC.Template;

namespace LogsCollections.EC.LogTypeManager
{
    class LogPathSetsMgr : MgrBase
    {
        private static readonly LogPathSetsMgr Instance = SingletonProvider<LogPathSetsMgr>.GetInstance();



        public static LogPathSetsMgr GetInstance()
        {
            return Instance;
        }


        public static LogPathSetsMgr GetInstance(string husDirPath)
        {
            _husInstalledDir = husDirPath;
            return Instance;
        }

        private static string _husInstalledDir;


        public ICollection<string> GetlogPathByType(LogType logTypeName)
        {

            var resultset = new HashSet<string>();
            string dirPath;
            switch (logTypeName)
            {
                case LogType.LogEc:
                    dirPath = _husInstalledDir + @"\Honeywell\HUS\EC\ecserverlog\";
                    resultset.Add(dirPath);
                    dirPath = _husInstalledDir + @"\Honeywell\HUS\EC\log\";
                    resultset.Add(dirPath);
                    dirPath = _husInstalledDir + @"\Honeywell\HUS\EC\TempLogs\";
                    resultset.Add(dirPath);
                    break;
                case LogType.LogAdapter:
                    dirPath = _husInstalledDir + @"\Honeywell\HUS\EC\devices\";
                    resultset.Add(dirPath);
                    dirPath = _husInstalledDir + @"\HUS\EC\SandboxFramework\ECLoader\Sandbox\devices\";
                    resultset.Add(dirPath);
                    break;
                case LogType.LogSandBox:
                    dirPath = _husInstalledDir + @"\HUS\EC\SandboxFramework\ECLoader\Sandbox\Logs\";
                    resultset.Add(dirPath);
                    break;
                case LogType.LogSysEvent:

                    break;
                case LogType.LogAll:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("logTypeName", logTypeName, null);
            }

            return resultset;
            //throw new NotImplementedException();
        }

    }
}
