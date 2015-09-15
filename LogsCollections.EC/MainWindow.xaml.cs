
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
        }


        //Checkbox and their status
        private readonly Dictionary<string, Status> _chechboxStatusDic = new Dictionary<string, Status>();

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var name = (CheckBox)sender;
            var keyname = name.Content.ToString();
            _chechboxStatusDic[keyname] = Status.IsChecked;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var name = (CheckBox)sender;
            var keyname = name.Content.ToString();
            if (_chechboxStatusDic.ContainsKey(keyname))
            {
                _chechboxStatusDic[keyname] = Status.IsUnChecked;
            }

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_chechboxStatusDic.Count == 0)
            {
                MessageBox.Show("木有勾选任何东西，你得瑟个啥！");
                return;
            }
            Start2CollectAllLogs();

            //throw new Exception("我就害你，我就抛异常!");
        }

        private void Start2CollectAllLogs()
        {
            _chechboxStatusDic.ToList().ForEach(item =>
            {
                if (item.Value == Status.IsChecked)
                {
                    StartToCollectLog(item.Key);
                }
            });
        }

        private void StartToCollectLog(string logCatagoryName)
        {
            var fullpath = GetfullPathbyCataGory(logCatagoryName);


            //throw new System.NotImplementedException();
        }


        private object GetfullPathbyCataGory(string logCatagoryName)
        {
            throw new System.NotImplementedException();
        }
    }

    public enum Status
    {
        IsChecked,
        IsUnChecked
    }



}
