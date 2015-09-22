using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace WPFProcessBar
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Thread.Sleep(1000);
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            updateprocess4();
            //updateprocess3();
            //updateprocess2();
            // updateprocess1();

            // updateprocess();

            //MessageBox.Show(ProgressBar1.Minimum.ToString(CultureInfo.InvariantCulture) + ProgressBar1.Maximum.ToString(CultureInfo.InvariantCulture));
            //var ok = MessageBox.Show("11111");
            //ProgressBar1.Value = 50;
            //if (ok == MessageBoxResult.OK)
            //{
            //    ProgressBar1.Value += 20;
            //}
        }

        private void updateprocess4()
        {
            ProgressBar1.Minimum = 0;

            ProgressBar1.Maximum = short.MaxValue;

            ProgressBar1.Value = 0;

            var averageStep = short.MaxValue/ProgressBar1.ActualWidth;

            var n = 0;

            while (ProgressBar1.Value < ProgressBar1.Maximum)
            {
                ProgressBar1.Value += 1;

                n += 1;

                if (!(n >= averageStep)) continue;
                Dispatcher.Invoke(() => { }, DispatcherPriority.Background);

                n = 0;
            }
        }

        private void updateprocess3()
        {
            ProgressBar1.Minimum = 0;

            ProgressBar1.Maximum = short.MaxValue;

            ProgressBar1.Value = 0;

            while (ProgressBar1.Value < ProgressBar1.Maximum)
            {
                ProgressBar1.Value += 1;
                Dispatcher.Invoke(() => { }, DispatcherPriority.Background);
            }
        }

        private void updateprocess2()
        {
            ProgressBar1.Minimum = 0;

            ProgressBar1.Maximum = short.MaxValue;

            ProgressBar1.Value = 0;

            while (ProgressBar1.Value < ProgressBar1.Maximum)
            {
                Dispatcher.Invoke(() => { ProgressBar1.Value += 1; }, DispatcherPriority.Background);
            }
        }

        private void updateprocess1()
        {
            ProgressBar1.Maximum = short.MaxValue; //模拟一个耗时计算

            ProgressBar1.Value = 0;

            while (ProgressBar1.Value < ProgressBar1.Maximum)
            {
                ProgressBar1.Value += 1;
                Thread.Sleep(10);
            }
        }

        private void updateprocess()
        {
            ProgressBar1.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                for (var i = 0; i < 100; i++)
                {
                    ProgressBar1.Value += 1;
                    Thread.Sleep(100);
                }

                if (ProgressBar1.Value < 100)
                {
                }
            }));
        }
    }
}