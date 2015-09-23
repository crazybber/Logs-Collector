using System;
using System.Collections.Generic;


namespace LogsCollections.EC
{
    class LogPathSetsMgr : ContextBoundObject
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
            switch (logTypeName)
            {
                case LogType.LogEc:
                    var ecdirPath = _husInstalledDir + @"\Honeywell\HUS\EC\ecserverlog\";
                    resultset.Add(ecdirPath);
                    ecdirPath = _husInstalledDir + @"\Honeywell\HUS\EC\log\";
                    resultset.Add(ecdirPath);
                    ecdirPath = _husInstalledDir + @"\Honeywell\HUS\EC\TempLogs\";
                    resultset.Add(ecdirPath);
                    break;
                case LogType.LogAdapter:
                    break;
                case LogType.LogSandBox:
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
