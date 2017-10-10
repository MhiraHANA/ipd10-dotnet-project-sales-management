using SMS.Model;
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
using System.Windows.Shapes;

namespace SMS
{
    /// <summary>
    /// Interaction logic for AddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : Window
    {
        Database db = new Database();
        public AddCustomer()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Customers cust = new Customers();
            cust.CompanyName = tbCompnayName.Text;
            cust.Address = tbAddress.Text;            
            cust.Phone = tbPhone.Text;
            cust.Email = tbEmail.Text;
            db.AddCustomer(cust);
            MessageBox.Show("Customer has been added succefully!");
            tbCompnayName.Clear();
            tbAddress.Clear();
            tbPhone.Clear();            
        }       

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
