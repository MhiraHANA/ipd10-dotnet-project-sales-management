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
    /// Interaction logic for AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Window
    {
        Database db = new Database();
        public AddOrder()
        {
            InitializeComponent();

            var listOfCustomers = new List<Customers>();
            listOfCustomers = db.GetAllCustomers();
      
            foreach (var item in listOfCustomers)
            {
                cmbCustomers.Items.Add(item);
         
                cmbCustomers.SelectedValuePath = "CustomerID";

            }
            var listOfEmployees = new List<Employees>();
            listOfEmployees = db.GetAllEmployees();

            foreach (var item in listOfEmployees)
            {
                cmbEmployees.Items.Add(item);

                cmbEmployees.SelectedValuePath = "EmployeeID";

            }
            var listOfProducts = new List<Products>();
            listOfProducts = db.GetAllProducts();

            foreach (var item in listOfProducts)
            {
                cmbProducts.Items.Add(item);

                cmbProducts.SelectedValuePath = "ProductID";

            }

        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            Orders o = new Orders();
            o.Address = tbAddress.Text;
            o.OrderDate = DateTime.Parse(tbOrderDate.Text);
            o.CustomerID= Int32.Parse(cmbCustomers.SelectedValue.ToString());
            o.EmployeeID= Int32.Parse(cmbEmployees.SelectedValue.ToString());
            o.ProductID= Int32.Parse(cmbProducts.SelectedValue.ToString());
            o.SellingPrice = float.Parse(tbSellingPrice.Text);
            o.Quantity = Int32.Parse(tbQuantity.Text);
            o.Discount = float.Parse(tbDiscount.Text);

            db.AddOrders(o);
            MessageBox.Show("Order has been added succefully!");
            tbAddress.Clear();
            tbDiscount.Clear();
            tbSellingPrice.Clear();
            tbQuantity.Clear();

            //tbOrderDate.Clear();
            cmbEmployees.SelectedIndex = -1;
            cmbCustomers.SelectedIndex = -1;
            cmbProducts.SelectedIndex = -1;
        }
        private void DatePicker_SelectedDateChanged(object sender,
         SelectionChangedEventArgs e)
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
