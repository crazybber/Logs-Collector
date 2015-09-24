

namespace LogsCollections.EC.EventArgs
{
    using System;
    class LogItemEventArgs : EventArgs
    {
     
        public double AverStepWidth { get; set; }
        public LogItemInfo LogItemInfo { get; set; }

        public LogItemEventArgs()
        {

        }

        public LogItemEventArgs(LogItemInfo logiteminfo,  double averStepWidth)
        {
            LogItemInfo = logiteminfo;
            AverStepWidth = averStepWidth;
        }
    }




}
