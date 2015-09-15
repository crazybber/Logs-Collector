using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace WPFDataTemple
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string name = "eamon chen";
        public MainWindow()
        {
            InitializeComponent();
        }
    }

    public class PersonList : ObservableCollection<Person>
    {
        public PersonList()
        {
            
            Add(new Person {Name = "eamon chen",Age = 24, Address = "上海" });
            Add(new Person { Name = "chen chen", Age = 20, Address = "上海" });
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }

        public override string ToString()
        {
            return string.Format("姓名:{0}, 年龄:{1}, 地址:{2}", Name, Age, Address);
            //return base.ToString();
        }
    }

}