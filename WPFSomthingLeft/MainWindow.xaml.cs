using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Threading;

namespace WPFSomthingLeft
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            //初始化
            Initialized += (sender, e) =>
            {
                Debug.WriteLine("窗体初始化完成 Initialized");
            };

            //激活
            Activated += (sender, e) =>
            {
                Debug.WriteLine("窗体被激活 Activated");
            };

            //加载
            Loaded += (sender, e) =>
            {
                Debug.WriteLine("窗体加载完成 Loaded");
            };

            //呈现内容
            ContentRendered += (sender, e) =>
            {
                Debug.WriteLine("呈现内容 ContentRendered");
            };

            //失活
            Deactivated += (sender, e) =>
            {
                Debug.WriteLine("窗体被失活 Deactivated");
            };

            //窗体获取输入焦点
            GotFocus += (sender, e) =>
            {
                Debug.WriteLine("窗体获取输入焦点 GotFocus");
            };

            //窗体失去输入焦点
            LostFocus += (sender, e) =>
            {
                Debug.WriteLine("窗体失去输入焦点 LostFocus");
            };

            //键盘获取输入焦点
            GotKeyboardFocus += (sender, e) =>
            {
                Debug.WriteLine("键盘获取输入焦点 GotKeyboardFocus");
            };

            //键盘失去输入焦点
            LostKeyboardFocus += (sender, e) =>
            {
                Debug.WriteLine("键盘失去输入焦点 LostKeyboardFocus");
            };

            //正在关闭
            Closing += (sender, e) =>
            {
                Debug.WriteLine("窗体正在关闭 Closeing");
            };

            //关闭
            Closed += (sender, e) =>
            {
                Debug.WriteLine("窗体正在关闭 Closed");
            };
        }

        public DispatcherTimer DisPatcherTimer = new DispatcherTimer();

        private void Button11_Click(object sender, RoutedEventArgs e)
        {
            DisPatcherTimer.Interval = TimeSpan.FromMilliseconds(1000);
            DisPatcherTimer.Tick += _disPatcherTimer_Tick;
            DisPatcherTimer.Start();
        }

        private void _disPatcherTimer_Tick(object sender, EventArgs e)
        {
            LabelTime1111.Content = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            // throw new NotImplementedException();
        }
    }
}