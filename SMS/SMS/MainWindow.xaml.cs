using Microsoft.Win32;
using SMS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Threading;
using System.Xml;
using System.Xml.XPath;
using LiveCharts;
using LiveCharts.Wpf;
namespace SMS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //PointLabel = chartPoint =>
        //    string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
        Database DB = new Database();
        public MainWindow()
        {
            InitializeComponent();

            FillDataGrid();
            //  startClock();
            FillDataGridSupplier();
            FillDataGridProducts();
            FillDataGridCustomers();
            FillDataGridOrder();



            ShowAccountCart();
        }
        public void ShowAccountCart()
        {
            Func<ChartPoint, string> labelPoint = chartPoint =>
                string.Format(" {0} ({1:P})", chartPoint.Y, chartPoint.Participation);


            lineChartProduct.Series.Clear();
            foreach (var entry in DB.getSellingProduct())
            {
                lineChartProduct.Series.Add(new PieSeries
                {
                    Title = entry.Key,
                    Values = new ChartValues<double> { entry.Value },
                    DataLabels = true,
                    LabelPoint = labelPoint


                });
                lineChartProduct.LegendLocation = LegendLocation.Bottom;
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


        /**********************************Customer***************************************/
        private void Show_AddCustomer(object sender, RoutedEventArgs e)
        {
            AddCustomer inputDialog = new AddCustomer();
            if (inputDialog.ShowDialog() == true)
            {


            }
            FillDataGridCustomers();
        }

        //Customer View
        private void FillDataGridCustomers()
        {

            string CmdString = "SELECT * FROM Customers";
            SqlCommand cmd = new SqlCommand(CmdString, DB.conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Customers");
            sda.Fill(dt);
            dgCustomers.ItemsSource = dt.DefaultView;

        }

        private void btnDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete?", "SMS DB", MessageBoxButton.YesNo);

            object item = dgCustomers.SelectedItem;
            string ID = (dgCustomers.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            int id = Convert.ToInt32(ID);
            if (id < 0)
            {

                MessageBox.Show("You must select customer.");
            }
            else
            {
                if (result == MessageBoxResult.Yes)
                {
                    DB.DeleteCustomer(id);
                    FillDataGridCustomers();
                    MessageBox.Show("Delete successful.");

                }

            }
        }
        private void Show_UpdateCustomer(object sender, RoutedEventArgs e)
        {
            object item = dgCustomers.SelectedItem;
            string ID = (dgCustomers.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            int id = Convert.ToInt32(ID);
            Customers cust = DB.GetCustomerById(id);
            UpdateCustomer inputDialog = new UpdateCustomer();
            inputDialog.id = id;
            inputDialog.tbCompnayName.Text = cust.CompanyName;
            inputDialog.tbAddress.Text = cust.Address;
            inputDialog.tbPhone.Text = cust.Phone;
            inputDialog.tbEmail.Text = cust.Email;
            if (inputDialog.ShowDialog() == true)
            {


            }
            FillDataGridCustomers();

        }



        private void SearchCustomer_Click(object sender, RoutedEventArgs e)
        {
            string querry = "Select * from Customers where CompanyName like '%" + tbSearchCustomer.Text + "'";
            SqlCommand cmd = new SqlCommand(querry, DB.conn);
            SqlDataAdapter da = new SqlDataAdapter(querry, DB.conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgCustomers.DataContext = dt;
            dgCustomers.ItemsSource = dt.DefaultView;
        }

        /**********************************Employee***************************************/
        private void Show_AddEmployee(object sender, RoutedEventArgs e)
        {
            AddEmployee inputDialog = new AddEmployee();
            if (inputDialog.ShowDialog() == true)
            {


            }
        }


        //Employee View
        private void FillDataGrid()
        {

            string CmdString = "SELECT * FROM Employees";
            SqlCommand cmd = new SqlCommand(CmdString, DB.conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Employees");
            sda.Fill(dt);
            dgEmployees.ItemsSource = dt.DefaultView;

        }
        //convert byte to image 
        public static BitmapImage ConvertByteArrayToBitmapImage(Byte[] bytes)
        {
            var stream = new MemoryStream(bytes);
            stream.Seek(0, SeekOrigin.Begin);
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = stream;
            image.EndInit();
            return image;
        }
        //The below method is actually converting the Image/Video to bytes array
        private static byte[] ConvertImageToBinary(string p_postedImageFileName)

        {

            try

            {

                FileStream fs = new FileStream(p_postedImageFileName, FileMode.Open, FileAccess.Read);

                BinaryReader br = new BinaryReader(fs);

                byte[] image = br.ReadBytes((int)fs.Length);

                br.Close();

                fs.Close();

                return image;

            }

            catch (Exception ex)

            {

                throw ex;

            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete?", "People DB", MessageBoxButton.YesNo);

            object item = dgEmployees.SelectedItem;
            string ID = (dgEmployees.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            int id = Convert.ToInt32(ID);
            if (id < 0)
            {

                MessageBox.Show("You must select Employee.");
            }
            else
            {
                if (result == MessageBoxResult.Yes)
                {
                    DB.DeleteEmployee(id);
                    FillDataGrid();
                    MessageBox.Show("Delete successful.");

                }

            }
        }
        private void Show_UpdateEmployee(object sender, RoutedEventArgs e)
        {

            object item = dgEmployees.SelectedItem;
            string ID = (dgEmployees.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            int id = Convert.ToInt32(ID);
            Employees em = DB.GetEmployeeById(id);
            UpdateEmployee inputDialog = new UpdateEmployee();
            inputDialog.id = id;
            inputDialog.tbFirstName.Text = em.FirstName;

            inputDialog.tbLastName.Text = em.LastName;
            inputDialog.tbAddress.Text = em.Address;
            inputDialog.tbHireDate.Text = em.HireDate.ToString();
            inputDialog.tbUserName.Text = em.UserName;
            inputDialog.tbPassword.Text = em.Password;
            inputDialog.tbPhone.Text = em.Phone;

            if (inputDialog.ShowDialog() == true)
            {


            }
            FillDataGrid();
        }



        //private void startClock()
        //{
        //    DispatcherTimer timer = new DispatcherTimer();
        //    timer.Interval = TimeSpan.FromSeconds(1);
        //    timer.Tick += tickevent;
        //    timer.Start();

        //}
        //private void tickevent(Object sender, EventArgs e)
        //{

        //    datelbl.Text = DateTime.Now.ToString();
        //}

        private void SearchEmployee_Click(object sender, RoutedEventArgs e)
        {
            string querry = "Select * from Employees where FirstName like '%" + tbSearch.Text + "'";
            SqlCommand cmd = new SqlCommand(querry, DB.conn);
            SqlDataAdapter da = new SqlDataAdapter(querry, DB.conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgEmployees.DataContext = dt;
            dgEmployees.ItemsSource = dt.DefaultView;
        }

        /********************************* Product **********************************/
        private void Show_AddProduct(object sender, RoutedEventArgs e)
        {
            AddProduct inputDialog = new AddProduct();
            if (inputDialog.ShowDialog() == true)
            {


            }
            FillDataGridProducts();
        }
        private void Show_ExportExcel(object sender, RoutedEventArgs e)
        {
            ExportToExcel();
        }

        private void ExportToExcel()
        {
            dgProducts.SelectAllCells();
            dgProducts.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dgProducts);
            String resultat = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            String result = (string)Clipboard.GetData(DataFormats.Text);
            dgProducts.UnselectAllCells();
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"../../FileExport/Products.xls");
            file.WriteLine(result.Replace(',', ' '));
            file.Close();

            MessageBox.Show(" Exporting DataGrid data to Excel file created");
        }
        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete?", "SMS DB", MessageBoxButton.YesNo);

            object item = dgProducts.SelectedItem;
            string ID = (dgProducts.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            int id = Convert.ToInt32(ID);
            if (id < 0)
            {

                MessageBox.Show("You must select Product.");
            }
            else
            {
                if (result == MessageBoxResult.Yes)
                {
                    DB.DeleteProduct(id);
                    FillDataGridProducts();
                    MessageBox.Show("Delete successful.");

                }

            }
        }

        private void Show_UpdateProduct(object sender, RoutedEventArgs e)
        {

            object item = dgProducts.SelectedItem;
            string ID = (dgProducts.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            int id = Convert.ToInt32(ID);
            Products p = DB.GetProductsById(id);
            UpdateProduct inputDialog = new UpdateProduct();
            inputDialog.id = id;
            inputDialog.tbProductName.Text = p.ProductName;
            inputDialog.tbQuantity.Text = p.Quantity.ToString();
            inputDialog.tbCostPrice.Text = p.CostPrice.ToString();
            inputDialog.tbUnitInStock.Text = p.UnitInStock.ToString();
            inputDialog.tbUnitOnOrder.Text = p.UnitInOrder.ToString();
            if (inputDialog.ShowDialog() == true)
            {


            }
            FillDataGridProducts();
        }

        private void btnUpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            FillDataGridProducts();
        }
        public void FillDataGridProducts()
        {

            string CmdString = "SELECT * FROM Products";
            SqlCommand cmd = new SqlCommand(CmdString, DB.conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Products");
            sda.Fill(dt);
            dgProducts.ItemsSource = dt.DefaultView;

        }


        private void SearchProduct_Click(object sender, RoutedEventArgs e)
        {
            string querry = "Select * from Products where ProductName like '%" + tbProductSearch.Text + "'";
            SqlCommand cmd = new SqlCommand(querry, DB.conn);
            SqlDataAdapter da = new SqlDataAdapter(querry, DB.conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgProducts.DataContext = dt;
            dgProducts.ItemsSource = dt.DefaultView;
        }
        //SeeMore_Click
        private void SeeMore_Click(object sender, RoutedEventArgs e)
        {
            object item = dgProducts.SelectedItem;
            string ID = (dgProducts.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            int ProductID = Convert.ToInt32(ID);
            switch (ProductID)
            {
                case 1:
                    Process.Start(getPhysicalPath(@"../../ProductsPDF/P01-04.pdf"));
                    break;
                case 2:
                    Process.Start(getPhysicalPath(@"../../ProductsPDF/P01-04.pdf"));
                    break;
                case 3:
                    Process.Start(getPhysicalPath(@"../../ProductsPDF/P01-04.pdf"));
                    break;
                case 4:
                    Process.Start(getPhysicalPath(@"../../ProductsPDF/P01-04.pdf"));
                    break;
                case 5:
                    Process.Start(getPhysicalPath(@"../../ProductsPDF/P05.pdf"));
                    break;
                case 6:
                    Process.Start(getPhysicalPath(@"../../ProductsPDF/P06.pdf"));
                    break;
                case 7:
                    Process.Start(getPhysicalPath(@"../../ProductsPDF/P07-08.pdf"));
                    break;
                case 8:
                    Process.Start(getPhysicalPath(@"../../ProductsPDF/P07-08.pdf"));
                    break;
                case 9:
                    Process.Start(getPhysicalPath(@"../../ProductsPDF/P09.pdf"));
                    break;
                case 10:
                    Process.Start(getPhysicalPath(@"../../ProductsPDF/P10.pdf"));
                    break;
                default:
                    Process.Start(getPhysicalPath(@"../../ProductsPDF/P01-04.pdf"));
                    break;
            }

        }
        private string getPhysicalPath(string relativePath)
        {

            return System.IO.Path.GetFullPath(relativePath);
        }
        /********************************* Suppliers **********************************/

        public void FillDataGridSupplier()
        {

            string CmdString = "SELECT * FROM Suppliers";
            SqlCommand cmd = new SqlCommand(CmdString, DB.conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Suppliers");
            sda.Fill(dt);
            dgSuppliers.ItemsSource = dt.DefaultView;

        }
        private void Show_AddSupplier(object sender, RoutedEventArgs e)
        {
            AddSupplier inputDialog = new AddSupplier();
            if (inputDialog.ShowDialog() == true)
            {

            }
            FillDataGridSupplier();


        }
        private void Show_UpdateSupplier(object sender, RoutedEventArgs e)
        {

            object item = dgSuppliers.SelectedItem;
            string ID = (dgSuppliers.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            int id = Convert.ToInt32(ID);
            Suppliers sup = DB.GetSupplierById(id);
            UpdateSupplier inputDialog = new UpdateSupplier();
            inputDialog.id = id;
            inputDialog.tbCompnayName.Text = sup.CompanyName;
            inputDialog.tbContactName.Text = sup.ContactName;
            inputDialog.tbAddress.Text = sup.SuppliersAddress;
            inputDialog.tbPhone.Text = sup.SuppliersPhone;

            if (inputDialog.ShowDialog() == true)
            {

            }
            FillDataGridSupplier();


        }
        private void btnDeleteSupplier_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete?", "SMS DB", MessageBoxButton.YesNo);
            object item = dgProducts.SelectedItem;
            string ID = (dgSuppliers.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            int id = Convert.ToInt32(ID);
            if (id < 0)
            {

                MessageBox.Show("You must select Supplier.");
            }
            else
            {
                if (result == MessageBoxResult.Yes)
                {
                    DB.DeleteSupplier(id);
                    FillDataGridSupplier();
                    MessageBox.Show("Delete successful.");

                }

            }
        }
        private void btnUpdateSupplier_Click(object sender, RoutedEventArgs e)

        {
            FillDataGridSupplier();
        }


        private void SearchSupplier_Click(object sender, RoutedEventArgs e)
        {
            string querry = "Select * from Suppliers where CompanyName like '%" + tbSupplierSearch.Text + "'";
            SqlCommand cmd = new SqlCommand(querry, DB.conn);
            SqlDataAdapter da = new SqlDataAdapter(querry, DB.conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgProducts.DataContext = dt;
            dgProducts.ItemsSource = dt.DefaultView;
        }



        /*************************************Order********************/

        public void FillDataGridOrder()
        {

            string CmdString = "SELECT * FROM Orders";
            SqlCommand cmd = new SqlCommand(CmdString, DB.conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Orders");
            sda.Fill(dt);
            dgOrders.ItemsSource = dt.DefaultView;

        }
        public void FillDataGridOrderDetailsProduct()
        {
            object item = dgOrders.SelectedItem;
            string ID = (dgOrders.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            int id = Convert.ToInt32(ID);
            Products prod = DB.GetProductByOrderIdProductId(id);
            DataSet dataSet = new DataSet();
            DataTable dt = new DataTable("Products");
            //   dataSet.Tables.Add(dt);
            dataSet.Tables["Products"].Rows.Add(prod);

        }
        private void Show_AddOrder(object sender, RoutedEventArgs e)

        {
            AddOrder inputDialog = new AddOrder();
            if (inputDialog.ShowDialog() == true)
            {


            }
            FillDataGridOrder();

        }
        private void btnDeleteOrder_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete?", "SMS DB", MessageBoxButton.YesNo);

            object item = dgOrders.SelectedItem;
            string ID = (dgOrders.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            int id = Convert.ToInt32(ID);
            if (id < 0)
            {

                MessageBox.Show("You must select Orders.");
            }
            else
            {
                if (result == MessageBoxResult.Yes)
                {
                    DB.DeleteOrders(id);
                    FillDataGridOrder();
                    MessageBox.Show("Delete successful.");

                }

            }
        }

        private void Show_OrderUpdate(object sender, RoutedEventArgs e)
        {

            object item = dgOrders.SelectedItem;
            string ID = (dgOrders.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            int id = Convert.ToInt32(ID);

            Orders o = DB.GetOrdersById(id);
            UpdateOrder inputDialog = new UpdateOrder();
            inputDialog.id = id;
            inputDialog.tbAddress.Text = o.Address;
            inputDialog.tbQuantity.Text = o.Quantity.ToString();
            inputDialog.tbSellingPrice.Text = o.SellingPrice.ToString();
            inputDialog.tbDiscount.Text = o.Discount.ToString();
            inputDialog.cmbCustomers.SelectedValue = o.CustomerID;
            inputDialog.cmbEmployees.SelectedValue = o.EmployeeID;
            inputDialog.cmbProducts.SelectedValue = o.ProductID;


            if (inputDialog.ShowDialog() == true)
            {


            }
            FillDataGridOrder();
        }
        private void SearchOrder_Click(object sender, RoutedEventArgs e)
        {
            string querry = "Select * from Orders where OrderDate like '%" + tbProductSearch.Text + "'";
            SqlCommand cmd = new SqlCommand(querry, DB.conn);
            SqlDataAdapter da = new SqlDataAdapter(querry, DB.conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgOrders.DataContext = dt;
            dgOrders.ItemsSource = dt.DefaultView;
        }


        private void ShowMoreDetailProduct(object sender, RoutedEventArgs e)

        {
            object item = dgOrders.SelectedItem;
            string ID = (dgOrders.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text;
            int id = Convert.ToInt32(ID);
            Products prod = DB.GetProductsById(id);
            // TODOD add oemploye details 
            DetailProduct inputDialog = new DetailProduct();

            inputDialog.tbProductName.Text = prod.ProductName;
            inputDialog.tbCostPrice.Text = prod.CostPrice.ToString();
            inputDialog.tbQuantity.Text = prod.Quantity.ToString();
            inputDialog.tbUnitInStock.Text = prod.UnitInStock.ToString();
            inputDialog.tbUnitOnOrder.Text = prod.UnitInOrder.ToString();


            String idCustomer = (dgOrders.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
            int custId = Convert.ToInt32(idCustomer);
            Customers cust = DB.GetCustomerById(custId);

            inputDialog.tbCompnayName.Text = cust.CompanyName.ToString();
            inputDialog.tbAddress.Text = cust.Address.ToString();
            inputDialog.tbPhone.Text = cust.Phone.ToString();
            inputDialog.tbEmail.Text = cust.Email.ToString();
            if (inputDialog.ShowDialog() == true)
            {


            }


        }

        private void ShowInvoice(object sender, RoutedEventArgs e)
        {

            /*Product*/
            object item = dgOrders.SelectedItem;
            string ID = (dgOrders.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text;
            int id = Convert.ToInt32(ID);
            Products prod = DB.GetProductsById(id);
            /*Orders*/
            string idOrders = (dgOrders.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            int idOd = Convert.ToInt32(idOrders);
            Orders o = DB.GetOrdersById(idOd);
            /*Employe*/
            string idEmp = (dgOrders.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
            int idemp = Convert.ToInt32(idEmp);
            Employees emp = DB.GetEmployeeById(idemp);
            /**Customers*/
            string idCust = (dgOrders.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
            int idcust = Convert.ToInt32(idEmp);
            Customers c = DB.GetCustomerById(idcust);

            /*Products*/
            string idProd = (dgOrders.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text;
            int idprod = Convert.ToInt32(idProd);
            Products p = DB.GetProductsById(idprod);

            Invoice inputDialog = new Invoice();
            inputDialog.lblOrderIDContent.Content = idOrders;
            inputDialog.lblCompnayNameContent.Content = c.CompanyName.ToString();
            inputDialog.lblAddressContent.Content = c.Address.ToString();
            inputDialog.lblPhoneContent.Content = c.Phone.ToString();
            inputDialog.lblDateContent.Content = (dgOrders.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text;
            inputDialog.lblEmployeeIDContent.Content = emp.FirstName.ToString() + " " + emp.LastName.ToString();
            Products t = DB.GetProductsById(idprod);


            inputDialog.lsvInvoice.Items.Add(p);

            if (inputDialog.ShowDialog() == true)
            {


            }
        }
        /***************************Repport *************************************/
        private void Show_AddReport(object sender, RoutedEventArgs e)
        {
            AddCustomer inputDialog = new AddCustomer();
            if (inputDialog.ShowDialog() == true)
            {


            }
        }


        private void ImportFromXMLFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML files (*.XML)|*.xml|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                using (StreamReader XmlFile = new StreamReader(openFileDialog.OpenFile()))
                {

                   
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(openFileDialog.FileName);

                    try
                    {
                        XmlNodeList dataFile = xmlDoc.SelectNodes("/catalog/book");

                        foreach (XmlNode node in dataFile)
                        {

                        }

                    }
                    catch (XPathException ex)
                    {
                        MessageBox.Show("There is a problem in Reading the XML File!", "File Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    DataSet dataSet = new DataSet();
                    dataSet.ReadXml(XmlFile);
                    DataView dataView = new DataView(dataSet.Tables[0]);
                    dgProducts.ItemsSource = dataView;

                }
                
            }
            else
            {
                MessageBox.Show("There is a problem in Reading the File!", "File Error", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void tbSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            tbSearch.Text = "";
            tbSearch.Foreground = Brushes.Black;
        }

        private void tbSearchCustomer_GotFocus(object sender, RoutedEventArgs e)
        {
            tbSearchCustomer.Text = "";
            tbSearchCustomer.Foreground = Brushes.Black;
        }

        private void tbOrderSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            tbOrderSearch.Text = "";
            tbOrderSearch.Foreground = Brushes.Black;
        }

        private void tbProductSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            tbProductSearch.Text = "";
            tbProductSearch.Foreground = Brushes.Black;
        }

        private void tbSupplierSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            tbSupplierSearch.Text = "";
            tbSupplierSearch.Foreground = Brushes.Black;
        }

       

    }
}
