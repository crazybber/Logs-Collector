using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;


namespace WPFSomthingLeft
{
    public class CustomTextBlock : TextBlock
    {

        public static DependencyProperty TimeProperty = DependencyProperty.Register(
            "Time",
            typeof(DateTime),
            typeof(CustomTextBlock),
            new PropertyMetadata(DateTime.Now, OnTimerPropertyChanged),
            ValidateTimeValue);

        private Timer _updateTimer;
        public CustomTextBlock()
        {
            _updateTimer = new Timer(RefreshTime, null, 0, 1000); 

        }
        private static bool ValidateTimeValue(object value)
        {
            var dt = Convert.ToDateTime(value);

            return dt.Year > 1990 && dt.Year < 2200;
            //throw new NotImplementedException();
        }

        [Description("获取或设置当前日期和时间")]
        [Category("Common Properties")]
        public DateTime Time
        {
            get
            {
                return (DateTime)GetValue(TimeProperty);
            }
            set
            {
                //给当前的属性赋值
                SetValue(TimeProperty, value);
            }

        }

        private static void OnTimerPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //  clock.OnTimeUpdated((DateTime)e.OldValue, (DateTime)e.NewValue);
            //throw new NotImplementedException();
        }

        private void RefreshTime(object stateInfo)
        {
           // var std = new Action<DateTime>(SetTimerProperty);
            Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action<DateTime>(datetime =>
            {
                Time = datetime;
            }), DateTime.Now);
        }


       


    }
}
