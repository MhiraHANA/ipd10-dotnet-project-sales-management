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
    /// Interaction logic for UpdateCustomer.xaml
    /// </summary>
    public partial class UpdateCustomer : Window
    {
        public int id;
        Database db = new Database();
        public UpdateCustomer()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            
            Customers cust = db.GetCustomerById(id);
            //dispaly text from database
            tbCompnayName.Text = cust.ComanyName;
            tbAddress.Text = cust.Address;
            tbPhone.Text = cust.Phone;
            

            // data after update
            cust.CompanyName = tbCompnayName.Text;
            cust.Address = tbAddress.Text;            
            cust.Phone = tbPhone.Text;            
            db.UpdateEmployee(cust);
            MessageBox.Show("Succeful adding customer..");
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
