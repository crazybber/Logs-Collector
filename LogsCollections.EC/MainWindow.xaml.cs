
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;


namespace LogsCollections.EC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        private double _averageStepwidth;


        public MainWindow()
        {

            InitializeComponent();
            InitSystemInfo();
            SetProcessBarInfo();
        }

        private void SetProcessBarInfo()
        {
            _averageStepwidth = (ProgressBar1.Maximum - ProgressBar1.Minimum) / ProgressBar1.Width;
        }
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

        private string _husInstalledDir;
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
            Start2CollectAllLogs();

            //throw new Exception("我就害你，我就抛异常!");
        }

        private void Start2CollectAllLogs()
        {
            var dicEntryList = _cbLogTypeItemInfoDic.Where(item => item.Value.LogItemStatus == Status.IsChecked).ToList();

            dicEntryList.ForEach(item =>
            {
                if (item.Value.LogItemStatus != Status.IsChecked) return;
              
                StartToCollectLog(new WapperEventArgs(item.Value));
            });
        }


        private void StartToCollectLog(WapperEventArgs eventArgs)
        {

            var itemInfo = eventArgs.logitemInfo;
            //  var fullpath = GetfullPathbyCataGory(itemInfo);
            if (itemInfo.LogItemPaths == null)
            {
                var pathListSets = GetlogPathByType(itemInfo.LogTypeName);
                itemInfo.LogItemPaths = pathListSets;
            }
            if (itemInfo.LogItemPaths.Count <= 0) return;


            var dicEntry = itemInfo.LogItemPaths.ToList();

            dicEntry.ForEach(dirpath =>
             {
                 FileCollectionMgr.CollectFilesAndZipThem(dirpath);
                 //

                 UpdateProgressBar();
             });
            //throw new System.NotImplementedException();

        }


        private ICollection<string> GetlogPathByType(LogType logTypeName)
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
                case LogType.LogSysEven:
                    break;
            }

            return resultset;
            //throw new NotImplementedException();
        }

        private void UpdateProgressBar()
        {

            ProgressBar1.Dispatcher.Invoke(() =>
            {
                ProgressBar1.Value += 1;

            }, DispatcherPriority.Normal);
        }

    }



}
