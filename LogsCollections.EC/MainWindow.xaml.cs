
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.Zip;

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

        private void InitSystemInfo()
        {
            //get All the needed system information.
            if (Environment.Is64BitOperatingSystem)
            {
                _husInstalledDir = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
                this.SytemInfo.Content += " Current OS 64bit";
            }
            else
            {
                _husInstalledDir = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                this.SytemInfo.Content += "Current OS 32bit";
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

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            var logtype = (LogType)((CheckBox)sender).Tag;
            if (_cbLogTypeItemInfoDic.ContainsKey(logtype))
            {
                _cbLogTypeItemInfoDic[logtype].LogItemStatus = Status.IsUnChecked;
            }

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
            _cbLogTypeItemInfoDic.ToList().ForEach(item =>
            {
                if (item.Value.LogItemStatus == Status.IsChecked)
                {
                    StartToCollectLog(item.Value);
                }
            });
        }

        private void StartToCollectLog(LogItemInfo itemInfo)
        {

            //  var fullpath = GetfullPathbyCataGory(itemInfo);
            if (itemInfo.LogItemPaths == null)
            {
                var pathListSets = GetlogPathByType(itemInfo.LogTypeName);
                itemInfo.LogItemPaths = pathListSets;
            }

            if (itemInfo.LogItemPaths.Count <= 0) return;
            var fastzip = new FastZip();
            itemInfo.LogItemPaths.ToList().ForEach(dirpath =>
            {
                CollectFilesAndZipThem(dirpath);
                UpdateProgressBar();
            });
            //throw new System.NotImplementedException();

        }
        


        private void CollectFilesAndZipThem(string dirpath)
        {
            var fastzip = new FastZip();
            fastzip.CreateZip("test.zip", dirpath, false, null);

            //throw new NotImplementedException();
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

        }

    }



}
