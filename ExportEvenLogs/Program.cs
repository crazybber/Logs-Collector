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

            var src = ExportEventLog2File.getLogSource();

            var text = ExportEventLog2File.ReadEventLog(src);

            ExportEventLog2File.Write2File("c:\\", text);



        }
    }
}
