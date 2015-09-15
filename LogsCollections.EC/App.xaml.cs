using System;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace LogsCollections.EC
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);


            DispatcherUnhandledException += App_DispatcherUnhandledException;

        }

        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var msg = new StringBuilder("哪个娃惹祸了，你把我给搞挂了！：\n");
            msg.AppendLine(e.Exception.Message);
            msg.AppendLine("别看了，我都死了，你Y快重启我!");
            MessageBox.Show(msg.ToString());
            e.Handled = true;
            // throw new NotImplementedException();
        }
    }
}
