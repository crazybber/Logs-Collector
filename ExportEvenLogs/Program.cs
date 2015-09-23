using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportEvenLogs
{
    class Program
    {
        static void Main(string[] args)
        {
            //  var evenMgr = 
            var evtLog = new EventLog("Application") { MachineName = "." }; // Event Log type
            // dot is local machine
            
            foreach (EventLogEntry evtEntry in evtLog.Entries)
            {
                Console.WriteLine(evtEntry.Message);
            }

            evtLog.Close();



        }
    }
}
