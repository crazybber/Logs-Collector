



namespace LogsCollections.EC.LogTypeManager
{

    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using Template;


    public class SystemEventLogMgr : FilesMgrBase
    {

        private static readonly SystemEventLogMgr Instance = SingletonProvider<SystemEventLogMgr>.GetInstance();



        public static SystemEventLogMgr GetInstance()
        {
            return Instance;
        }


        public void CollectSystemLog()
        {

            //STEP 1 CheckLogDestinationDir
            var curLogTypeRootDir = GetCurrentTypeLogRootDir();

            //check dir location.
            CreateIfNotExist(curLogTypeRootDir);
            //Step 2 export Systemlog to typeLogDir

            int count2collect;
            getLogCollectSetting(out  count2collect);

            var systemlogfullpath = Path.Combine(curLogTypeRootDir, "systemevent.log");

            ExportAppEventLogs(systemlogfullpath, count2collect);

            // STEP 3 zip them 

            FileCollectZipMgr.zipdir(curLogTypeRootDir);

            //step 4 delete

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
                if (entry.EntryType != EventLogEntryType.Error | entry.EntryType != EventLogEntryType.Warning)
                    continue;

                sb.AppendFormat("消息:\n{0};时间:\n{1};来源:\n{2};类型:\n{3};"
                     , entry.Message
                     , entry.TimeGenerated.ToString("yyyy-MM-dd HH:mm:ss.fff")
                     , entry.Source
                     , entry.EntryType
                     );
                sb.AppendLine();

            }
            eventLog.Close();

            return sb.ToString();
        }


        public override string GetCurrentTypeLogRootDir()
        {
            var colletdir = GetNextAllLogTypeRootDirFullPath();

            var systemdirname = Path.Combine(colletdir, "SystemLog");

            return systemdirname;

        }

        private void getLogCollectSetting(out int numtoCollect)
        {
            numtoCollect = 0;
            // TODO.....


            if (numtoCollect <= 0)
            {
                numtoCollect = 1000;
            }
        }


    }
}
