



namespace LogsCollections.EC.LogTypeManager
{
   
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using Template;


    class SystemEventLogMgr : MgrBase
    {

        private static readonly SystemEventLogMgr Instance = SingletonProvider<SystemEventLogMgr>.GetInstance();



        public static SystemEventLogMgr GetInstance()
        {
            return Instance;
        }

        public void ExportAppEventLogs(string path, int itemNum)
        {

            var itemContent = ReadEventLog(getLogSource(), itemNum);

            Write2File(path, itemContent);


        }


        /// <summary>
        /// Write EventLog to Local  Files
        /// </summary>
        /// <param name="path">path to write</param>
        /// <param name="content"> log conent </param>
        public void Write2File(string path, string content)
        {

            using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (var sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.Write(content);
                }
            }
        }

        public EventLog getLogSource(string LogCatagory = "Application", string serverName = ".")
        {
            return new EventLog(LogCatagory) { MachineName = "." }; // SEvent Log type
            // dot is local machine
        }

        public string ReadEventLog(EventLog eventLog, int logcount = 1000)
        {


            var count = logcount;
            var total = eventLog.Entries.Count - 1;
            var sb = new StringBuilder();

            while (count-- > 0)
            {
                var entry = eventLog.Entries[total--];
                sb.AppendFormat("消息:{0};时间:{1};来源:{2};类型:{3};"
                     , entry.Message
                     , entry.TimeGenerated.ToString("yyyy-MM-dd HH:mm:ss.fff")
                     , entry.Source
                     , entry.EntryType.ToString()
                     );
                sb.AppendLine();

            }
            eventLog.Close();

            return sb.ToString();
        }
    }
}
