using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SMS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Show_AddEmployee(object sender, RoutedEventArgs e)
        {
            AddEmployee inputDialog = new AddEmployee();
            if (inputDialog.ShowDialog() == true)
            {
                

            }
        }
     

        private void DatePicker_SelectedDateChanged(object sender,
           SelectionChangedEventArgs e)
        {
            // ... Get DatePicker reference.
            var picker = sender as DatePicker;

            // ... Get nullable DateTime from SelectedDate.
            DateTime? date = picker.SelectedDate;
            if (date == null)
            {
                // ... A null object.
                this.Title = "No date";
            }
            else
            {
                // ... No need to display the time.
                this.Title = date.Value.ToShortDateString();
            }
        }

        private void Show_AddCustomer(object sender, RoutedEventArgs e)
        {
            AddCustomer inputDialog = new AddCustomer();
            if (inputDialog.ShowDialog() == true)
            {


            }
        }

        private void Show_AddProduct(object sender, RoutedEventArgs e)
        {
            AddCustomer inputDialog = new AddCustomer();
            if (inputDialog.ShowDialog() == true)
            {


            }
        }

        private void Show_AddOrder(object sender, RoutedEventArgs e)
        {
            AddCustomer inputDialog = new AddCustomer();
            if (inputDialog.ShowDialog() == true)
            {


            }
        }

        private void Show_AddReport(object sender, RoutedEventArgs e)
        {
            AddCustomer inputDialog = new AddCustomer();
            if (inputDialog.ShowDialog() == true)
            {


            }
        }

        private void Show_AddSupplier(object sender, RoutedEventArgs e)
        {
            AddCustomer inputDialog = new AddCustomer();
            if (inputDialog.ShowDialog() == true)
            {


            }
        }
    }
}
