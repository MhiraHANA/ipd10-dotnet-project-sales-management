using SMS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            var listOfSuppliers = new List<Suppliers>();
            listOfSuppliers = db.GetAllSuppliers();
          //  cmbSuppliers.Items.Clear();
            foreach (var item in listOfSuppliers)
            {

                   cmbSuppliers.Items.Add(item);
               
                  cmbSuppliers.SelectedValuePath = "SupplierID";

            }
           


        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //SupplierID, ProductName, Quantity, CostPrice, UnitInStock, UnitInOrder
            Products p = new Products();
            p.ProductName = tbProductName.Text ;
            p.Quantity = Convert.ToInt64((tbQuantity.Text));
            p.UnitInOrder = Int32.Parse(tbUnitOnOrder.Text);
            p.UnitInStock= Int32.Parse(tbUnitInStock.Text);
            p.CostPrice = float.Parse(tbCostPrice.Text);
            p.SupplierID = Int32.Parse(cmbSuppliers.SelectedValue.ToString());
            db.AddProduct(p);
            MessageBox.Show("Product has been added succefully!");
            tbProductName.Clear();
            tbQuantity.Clear();
            tbUnitOnOrder.Clear();
            tbCostPrice.Clear();
            tbUnitInStock.Clear();
            tbUnitOnOrder.Clear();
            cmbSuppliers.SelectedIndex = -1;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
