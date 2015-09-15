using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using WPFControl.Annotations;

namespace WPFControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {

        private ObservableCollection<Person> _personList = new ObservableCollection<Person>();

        public ObservableCollection<Person> PersonList
        {
            get { return _personList; }
            set { _personList = value; }
        }

        public MainWindow()
        {
            InitializeComponent();


            for (var i = 0; i < 100; i++)
            {

                var tbx = new TextBox { Text = i.ToString() };

                TestStack.Children.Add(tbx);
            }

            _personList.Add(new Person { Name = "Eamon", Age = 24 });

            _personList.Add(new Person { Name = "Chen", Age = 21 });

           

        }

        private void Button1_OnClick(object sender, RoutedEventArgs e)
        {
            var num = Convert.ToInt32(TextBox1.Text);
            TextBox1.Text = (++num).ToString();
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            var num = Convert.ToInt32(TextBox1.Text);
            TextBox1.Text = (++num).ToString();
        }

        private void CheckBox1_OnIndeterminate(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("不错");
            //throw new NotImplementedException();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _personList.RemoveAt(0);
        }


        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            var first = _personList.FirstOrDefault();
            if (first != null) first.Name = TextBox11.Text;
            // throw new NotImplementedException();
        }
    }



    public class Person:INotifyPropertyChanged
    {
        public string name;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public int age;

        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
                OnPropertyChanged("Age");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}