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
    /// Interaction logic for UpdateProduct.xaml
    /// </summary>
    public partial class UpdateProduct : Window
    {

        public int id;
        Database db = new Database();
        public UpdateProduct()
        {
            InitializeComponent();
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

         
            Products p = db.GetProductsById(id);
        
            p.ProductID = id;
            p.ProductName = tbProductName.Text;
            p.Quantity = Convert.ToInt64((tbQuantity.Text));
            p.UnitInOrder = Int32.Parse(tbUnitOnOrder.Text);
            p.UnitInStock = Int32.Parse(tbUnitInStock.Text);
            p.CostPrice = float.Parse(tbCostPrice.Text);
            p.SupplierID = Int32.Parse(cmbSuppliers.SelectedValue.ToString());
            db.UpdateProduct(p);
            MessageBox.Show("Succeful updating product..");

        }
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get DatePicker reference.
            var picker = sender as DatePicker;

            // ... Get nullable DateTime from SelectedDate.
            DateTime? date = picker.SelectedDate;

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
