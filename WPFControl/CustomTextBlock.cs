using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPFControl.Annotations;

namespace WPFControl
{
    public class CustomTextBlock : TextBlock
    {

        public static readonly DependencyProperty TimerProperty = DependencyProperty.Register(
            "Time", typeof(DateTime), typeof(CustomTextBlock), new PropertyMetadata(DateTime.Now, OnTimerPropertyChanged), ValidateTimeValue);

        private static bool ValidateTimeValue(object value)
        {
            var dt = (DateTime)value;
            return dt.Year > 1990 && dt.Year < 2200;
            // throw new NotImplementedException();
        }

        private static void OnTimerPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }
       
        public DateTime Time
        {
            get { return (DateTime)GetValue(TimerProperty); }
            set
            {
                SetValue(TimerProperty, value);
            }
        }

      
    }
}
