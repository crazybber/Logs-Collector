

using System.Collections.Generic;

namespace LogsCollections.EC
{
    class TypeDefine
    {
    }


    public class LogItemInfo
    {
        public string LogItemName { get; set; }
        public Status LogItemStatus { get; set; }

        public LogType LogTypeName { get; set; }

        public ICollection<string> LogItemPaths { get; set; }

        //public LogItemInfo()
        //{
        //    LogItemPaths = new HashSet<string>();
        //}

        //public void AddLogPath(string path)
        //{
        //    if (path != null && LogItemPaths.Contains(path))
        //    {
        //        LogItemPaths.Add(path);
        //    }
        //}

        //public void RemoveLogPath(string path)
        //{
        //    if (path != null && LogItemPaths.Contains(path))
        //    {
        //        LogItemPaths.Remove(path);
        //    }
        //}

    }


    public enum Status
    {
        IsChecked,
        IsUnChecked
    }

    public enum LogType
    {
        LogEc,
        LogSandBox,
        LogAdapter,
        LogSysEven,
        LogAll
    }
}
