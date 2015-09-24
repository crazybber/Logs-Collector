namespace LogsCollections.EC.EventArgs
{
    internal class ProcessBarEventArgs : LogItemEventArgs
    {
        public double InnerAverStepWidth { get; set; }
        public double CurrentInnerIndex { get; set; }

        public ProcessBarEventArgs()
        {
        }

        public ProcessBarEventArgs(LogItemInfo logitemInfo,
                                    double averageStepWidth,
                                    double innerAverStepWidth,
                                    double innerIndexOrder)
            : base(logitemInfo, averageStepWidth)
        {
            InnerAverStepWidth = innerAverStepWidth;
            CurrentInnerIndex = innerIndexOrder;
        }
    }
}