using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace WPFDotNetObjectBindingControl
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            var list = new List<Student>();


            for (var i = 20; i < 30; i++)
            {
                list.Add(new Student {Name = "Eamon" + i, Age = i});
            }
            ListView1.ItemsSource = list;
        }
    }

    public class ColorConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = value as ListViewItem;

            var view = ItemsControl.ItemsControlFromItemContainer(item) as ListView;
            if (item == null || view == null) throw new NotImplementedException();
            var index = view.ItemContainerGenerator.IndexFromContainer(item);
            var student = view.Items[index] as Student;
            if (student != null && student.Age == 22)
            {
                return Brushes.Red;
            }
            return index%2 == 0 ? Brushes.Pink : Brushes.Blue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
            //  throw new NotImplementedException();
        }
    }


    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}