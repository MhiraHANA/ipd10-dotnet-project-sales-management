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
    /// Interaction logic for UpdateSupplier.xaml
    /// </summary>
    public partial class UpdateSupplier : Window
    {
        public UpdateSupplier()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Suppliers sup = db.GetSupplierById(id);
            sup.SupplierID = id;
            sup.CompanyName = tbCompnayName.Text;
            sup.ContactName = tbContactName.Text;
            sup.Address = tbAddress.Text;
            sup.Phone = tbPhone.Text;            
            db.UpdateSupplier(sup);
            MessageBox.Show("The supplier has been successfully updated");
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
