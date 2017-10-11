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
    /// Interaction logic for AddSupplier.xaml
    /// </summary>
    public partial class AddSupplier : Window
    {
        Database db = new Database();
        public AddSupplier()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Suppliers s = new Suppliers();
            s.CompanyName = tbCompnayName.Text;
            s.ContactName = tbContactName.Text;
            s.SuppliersAddress = tbAddress.Text;
            s.SuppliersPhone = tbPhone.Text;
            db.AddSupplier(s);
            MessageBox.Show("The supplier has been added succefully.");
            tbCompnayName.Clear();
            tbContactName.Clear();
            tbAddress.Clear();       
            tbPhone.Clear();

           
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
