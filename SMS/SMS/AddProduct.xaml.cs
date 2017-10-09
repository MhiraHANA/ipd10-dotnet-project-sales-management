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
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        Database db = new Database();

        public AddProduct()
        {
            InitializeComponent();
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var listOfSuppliers = new List<Suppliers>();
            listOfSuppliers = db.GetAllSuppliers();
            cmbSuppliers.Items.Clear();
            foreach (var item in listOfSuppliers)
            {
                cmbSuppliers.Items.Add(new { name = Convert.ToString(item.CompanyName), value = Convert.ToString(item.SupplierID) });
            }
            cmbSuppliers.DisplayMemberPath = "CompanyName";
             cmbSuppliers.SelectedValuePath = "SupplierID";
         
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
