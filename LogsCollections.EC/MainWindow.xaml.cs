
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using LogsCollections.EC.EventArgs;
using LogsCollections.EC.LogTypeManager;


namespace LogsCollections.EC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        public MainWindow()
        {
            InitializeComponent();
            InitSystemInfo();
        }

        public string _husInstalledDir { get; set; }
        private void InitSystemInfo()
        {
            //get All the needed system information.
            if (Environment.Is64BitOperatingSystem)
            {
                _husInstalledDir = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
                SytemInfo.Content += " Current OS 64bit";
            }
            else
            {
                _husInstalledDir = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                SytemInfo.Content += "Current OS 32bit";
            }


            //throw new System.NotImplementedException();
        }


        //Checkbox and their status
        private readonly Dictionary<LogType, LogItemInfo> _cbLogTypeItemInfoDic = new Dictionary<LogType, LogItemInfo>();



        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var logtype = (LogType)((CheckBox)sender).Tag;
            if (!_cbLogTypeItemInfoDic.ContainsKey(logtype))
            {
                _cbLogTypeItemInfoDic[logtype] = new LogItemInfo
                {
                    CollecetdItemIndex = -1,
                    LogTypeName = logtype,
                    LogItemPaths = LogPathSetsMgr.GetInstance().GetlogPathByType(logtype),
                    LogItemStatus = Status.IsChecked

                };
            }
            _cbLogTypeItemInfoDic[logtype].LogItemStatus = Status.IsChecked;

            if (logtype.Equals(LogType.LogAll))
            {
                CheckAllUnCheckedBox();
            }


        }


        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            var logtype = (LogType)((CheckBox)sender).Tag;
            if (!_cbLogTypeItemInfoDic.ContainsKey(logtype)) return;
            _cbLogTypeItemInfoDic[logtype].LogItemStatus = Status.IsUnChecked;
            // _cbLogTypeItemInfoDic.Remove(logtype); //Remove element
        }

        private void CheckAllUnCheckedBox()
        {
            if (CheckBoxWrapPanel.Children.Count <= 0) return;
            foreach (var item in CheckBoxWrapPanel.Children.Cast<object>()
                .Select(child => child as CheckBox).
                Where(item => item != null && item.IsChecked != null && (bool)!item.IsChecked))
            {
                item.IsChecked = true;
            }
            // throw new System.NotImplementedException();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_cbLogTypeItemInfoDic.Count == 0)
            {
                MessageBox.Show("木有勾选任何东西，你得瑟个啥！");
                return;
            }

            CollectAllLogItems();

            //throw new Exception("我就害你，我就抛异常!");
        }




        private void CollectAllLogItems()
        {
            var dicEntryList = _cbLogTypeItemInfoDic
                                .Where(item => item.Value.LogItemStatus == Status.IsChecked && item.Value.LogItemPaths != null && item.Value.LogItemPaths.Count > 0)
                                .ToList();
            CheckingLogCSetting(dicEntryList);

            var averageStep = (ProgressBar1.Maximum - ProgressBar1.Minimum) / dicEntryList.Count;

            dicEntryList.ForEach(item =>
            {
                CollectLogItem(new LogItemEventArgs(item.Value, averageStep));
            });
        }

        private void CheckingLogCSetting(List<KeyValuePair<LogType, LogItemInfo>> dicEntryList)
        {
            var itemIndex = 0;
            //  var pathCount = 0;
            dicEntryList.ForEach(item =>
            {
                if (item.Value.LogItemStatus != Status.IsChecked) return;
                item.Value.CollecetdItemIndex = itemIndex++; //Save index order
                if (item.Value.LogItemPaths.Count <= 0)
                {
                    throw new ArgumentException(item.Key + ":LogItemPaths Count:0");
                }
                // pathCount += item.Value.LogItemPaths.Count;

            });

            // return pathCount;
            // throw new NotImplementedException();
        }



        private void CollectLogItem(LogItemEventArgs eventArgs)
        {
            if (eventArgs == null) throw new ArgumentNullException("LogItemEventArgs:" + "eventArgs");

            var itemInfo = eventArgs.LogItemInfo;
            var averageStep = eventArgs.AverStepWidth;


            //  var fullpath = GetfullPathbyCataGory(itemInfo);
            if (itemInfo.LogItemPaths == null)
            {
                var pathListSets = LogPathSetsMgr.GetInstance(_husInstalledDir).GetlogPathByType(itemInfo.LogTypeName);
                itemInfo.LogItemPaths = pathListSets;
            }
            if (itemInfo.LogItemPaths.Count <= 0) return;

            if (itemInfo.LogTypeName == LogType.LogSysEvent)
            {
               // SystemEventLogMgr.GetInstance().ExportAppEventLogs();
            }

            var pathEntryItems = itemInfo.LogItemPaths;
            var index = 0;
            var pathCount = pathEntryItems.Count;
            var innerStepWidth = averageStep / pathCount;

            pathEntryItems.ToList().ForEach(dirpath =>
             {
                 //   FileCollectZipMgr.CollectFilesAndZipThem(dirpath);
                 Thread.Sleep(800);
                 UpdateProgressBar(new ProcessBarEventArgs
                 {
                     LogItemInfo = itemInfo,
                     AverStepWidth = averageStep,
                     InnerAverStepWidth = innerStepWidth,
                     CurrentInnerIndex = index++
                 });
             });
            //throw new System.NotImplementedException();

        }




        private void UpdateProgressBar(ProcessBarEventArgs eventArgs)
        {
            if (eventArgs == null) throw new ArgumentNullException("ProcessBarEventArgs:" + "eventArgs");
            var averageStep = eventArgs.AverStepWidth;
            var stepIndex = eventArgs.LogItemInfo.CollecetdItemIndex;
            var innerIndex = eventArgs.CurrentInnerIndex;
            var innerStepWidth = eventArgs.InnerAverStepWidth;


            ProgressBar1.Dispatcher.Invoke(() =>
            {
                ProgressBar1.Value = ProgressBar1.Value + stepIndex * averageStep + innerIndex * innerStepWidth;

                var percent = ProgressBar1.Value / (ProgressBar1.Maximum - ProgressBar1.Minimum) * 100;

                if (percent > 99.99999) percent = 100;

                ProgressLabel.Text = "Collection Status: " + percent + "%";

            }, DispatcherPriority.Background);
        }
    }
}
