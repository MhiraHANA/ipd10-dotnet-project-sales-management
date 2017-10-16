using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model
{
    class Database
    {
        private string CONN_STRING = @"Server=tcp:h2m.database.windows.net,1433;Initial Catalog=SMSDB;Persist Security Info=False;User ID=sqladmin;Password=Sms@project;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public SqlConnection conn;

        public Database()
        {
            conn = new SqlConnection();
            conn.ConnectionString = CONN_STRING;
            conn.Open();
        }
        /****************************************************************Employee Section ***********************************************/
        Employees emp;
        public void AddEmployee(Employees emp)
        {

            string sql = "INSERT INTO Employees (LastName, FirstName, HireDate, Address, Photo, UserName, Password) VALUES "
                        + " (@LastName,@FirstName,@HireDate,@Address,@Photo,@UserName,@Password)";
            SqlCommand insertCommand = new SqlCommand(sql, conn);
            insertCommand.Parameters.Add(new SqlParameter("@LastName", emp.LastName));
            insertCommand.Parameters.Add(new SqlParameter("@FirstName", emp.FirstName));
            insertCommand.Parameters.Add(new SqlParameter("@HireDate", emp.HireDate));
            insertCommand.Parameters.Add(new SqlParameter("@Address", emp.Address));
            insertCommand.Parameters.Add(new SqlParameter("@Phone", emp.Phone));
            insertCommand.Parameters.Add(new SqlParameter("@Photo", emp.Photo));
            insertCommand.Parameters.Add(new SqlParameter("@UserName", emp.UserName));
            insertCommand.Parameters.Add(new SqlParameter("@Password", emp.Password));
            insertCommand.ExecuteNonQuery();
        }

        public void DeleteEmployee(int Id)
        {
            string sqlDelete = "DELETE FROM Employees WHERE @Id = Id;";
            SqlCommand deleteCommand = new SqlCommand(sqlDelete, conn);
            deleteCommand.Parameters.AddWithValue("@Id", Id);
            deleteCommand.ExecuteNonQuery();
        }

        public void UpdateEmployee(Employees emp)
        {
       
            string sql = "UPDATE Employees SET LastName=@LastName, FirstName=@FirstName , HireDate=@HireDate , Address=@Address , Phone=@Phone, Photo=@Photo, UserName=@UserName , Password=@Password  WHERE @EmployeeID = EmployeeID";
            SqlCommand updateCommand = new SqlCommand(sql, conn);
            updateCommand.Parameters.AddWithValue("@EmployeeID", emp.EmployeeID);
            updateCommand.Parameters.AddWithValue("@LastName", emp.LastName);
            updateCommand.Parameters.AddWithValue("@FirstName", emp.FirstName);
            updateCommand.Parameters.AddWithValue("@HireDate", emp.HireDate);
            updateCommand.Parameters.AddWithValue("@Address", emp.Address);
            updateCommand.Parameters.AddWithValue("@Phone", emp.Phone);
            updateCommand.Parameters.AddWithValue("@Photo", emp.Photo);
            updateCommand.Parameters.AddWithValue("@UserName", emp.UserName);
            updateCommand.Parameters.AddWithValue("@Password", emp.Password);
            updateCommand.ExecuteNonQuery();
        }
        public Employees GetEmployeeById(int id)
        {

            // Employees emp;
            string sqlDelete = "SELECT * FROM Employees WHERE @EmployeeID = EmployeeID;";
            SqlCommand Command = new SqlCommand(sqlDelete, conn);
            Command.Parameters.AddWithValue("@EmployeeID", id);
            using (var reader = Command.ExecuteReader())
            {
                if (reader.Read())
                {
                    emp = new Employees
                    {
                        LastName = reader["LastName"].ToString(),
                        FirstName = reader["FirstName"].ToString(),
                        HireDate = DateTime.Parse(reader["HireDate"].ToString()),
                        Address = reader["Address"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        UserName = reader["UserName"].ToString(),
                        Password = reader["Password"].ToString(),
                        Photo = (Byte[])reader["Photo"]
                    };
                }

            }

            return emp;
        }
        public List<Employees> GetAllEmployees()
        {
            SqlCommand selectCommand = new SqlCommand("SELECT * FROM Employees ORDER BY EmployeeID", conn);
            var listOfEmployees = new List<Employees>();
            using (SqlDataReader reader = selectCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                     emp = new Employees();
                    emp.EmployeeID = Convert.ToInt32(reader["EmployeeID"].ToString());
                    emp.LastName = reader["LastName"].ToString();
                    emp.FirstName = reader["FirstName"].ToString();
                    emp.HireDate =(DateTime) reader["HireDate"];
                    emp.Address = reader["Address"].ToString();
                    emp.Phone= reader["Phone"].ToString();
                    emp.UserName= reader["UserName"].ToString();
                    emp.Password = reader["Password"].ToString();
                    emp.Photo = (Byte[])reader["Photo"];

                    listOfEmployees.Add(emp);
                }
            }
            return listOfEmployees;
        }
        /**************************************************************** Customer Section ***********************************************/
        Customers cust;
        public void AddCustomer(Customers cust)
        {

            string sql = "INSERT INTO Customers (CompanyName, Address, Phone, Email) VALUES "
                        + " (@CompanyName,@Address,@Phone,@Email)";
            SqlCommand insertCommand = new SqlCommand(sql, conn);
            insertCommand.Parameters.Add(new SqlParameter("@CompanyName", cust.CompanyName));
            insertCommand.Parameters.Add(new SqlParameter("@Address", cust.Address));
            insertCommand.Parameters.Add(new SqlParameter("@Phone", cust.Phone));
            insertCommand.Parameters.Add(new SqlParameter("@Email", cust.Email));
            insertCommand.ExecuteNonQuery();
        }

        public void DeleteCustomer(int Id)
        {
            string sqlDelete = "DELETE FROM Customers WHERE @Id = Id;";
            SqlCommand deleteCommand = new SqlCommand(sqlDelete, conn);
            deleteCommand.Parameters.AddWithValue("@Id", Id);
            deleteCommand.ExecuteNonQuery();
        }

        public void UpdateCustomer(Customers cust)
        {
            string sql = "UPDATE Customers SET CompanyName = @CompanyName, Address = @Address, Phone = @Phone WHERE @CustomerID = CustomerID"; ;
            SqlCommand updateCommand = new SqlCommand(sql, conn);
            updateCommand.Parameters.AddWithValue("@CustomerID", cust.CustomerID);
            updateCommand.Parameters.AddWithValue("@CompanyName", cust.CompanyName);
            updateCommand.Parameters.AddWithValue("@Address", cust.Address);
            updateCommand.Parameters.AddWithValue("@Phone", cust.Phone);
            updateCommand.Parameters.AddWithValue("@Email", cust.Email);
            updateCommand.ExecuteNonQuery();
        }
        public Customers GetCustomerById(int id)
        {

            //Customers cust;
            string sqlDelete = "SELECT * FROM Customers WHERE @CustomerID = CustomerID;";
            SqlCommand Command = new SqlCommand(sqlDelete, conn);
            Command.Parameters.AddWithValue("@CustomerID", id);
            using (var reader = Command.ExecuteReader())
            {
                if (reader.Read())
                {
                    cust = new Customers
                    {
                        CompanyName = reader["CompanyName"].ToString(),
                        Address = reader["Address"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Email = reader["Email"].ToString()
                    };
                }
            }
            return cust;
        }
        public List<Customers> GetAllCustomers()
        {
            SqlCommand selectCommand = new SqlCommand("SELECT * FROM Customers ORDER BY CustomerID", conn);
            var listOfCustomers = new List<Customers>();
            using (SqlDataReader reader = selectCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    var s = new Customers();
                    s.CustomerID = Convert.ToInt32(reader["CustomerID"].ToString());
                    s.CompanyName = reader["CompanyName"].ToString();
                    s.Address = reader["Address"].ToString();
                    s.Phone = reader["Phone"].ToString();
                    s.Email = reader["Email"].ToString();
                   
                    listOfCustomers.Add(s);
                }
            }
            return listOfCustomers;

        }

        /**************************************************************** Supplier Section ***********************************************/
        Suppliers sup;
        public void AddSupplier(Suppliers sup)
        {

            string sql = "INSERT INTO Suppliers (CompanyName, ContactName, Address, Phone ) VALUES "
                        + " (@CompanyName,@ContactName,@Address,@Phone)";
            SqlCommand insertCommand = new SqlCommand(sql, conn);
            insertCommand.Parameters.Add(new SqlParameter("@CompanyName", sup.CompanyName));
            insertCommand.Parameters.Add(new SqlParameter("@ContactName", sup.ContactName));
            insertCommand.Parameters.Add(new SqlParameter("@Address", sup.SuppliersAddress));
            insertCommand.Parameters.Add(new SqlParameter("@Phone", sup.SuppliersPhone));            
            insertCommand.ExecuteNonQuery();
        }

        public void DeleteSupplier(int Id)
        {
            string sqlDelete = "DELETE FROM Suppliers WHERE @Id = Id;";
            SqlCommand deleteCommand = new SqlCommand(sqlDelete, conn);
            deleteCommand.Parameters.AddWithValue("@Id", Id);
            deleteCommand.ExecuteNonQuery();
        }

        public void UpdateSupplier(Suppliers sup)
        {
            string sql = "UPDATE Suppliers SET CompanyName = @CompanyName, ContactName = @ContactName, Address = @Address, Phone = @Phone WHERE @SupplierID = SupplierID"; ;
            SqlCommand updateCommand = new SqlCommand(sql, conn);
            updateCommand.Parameters.AddWithValue("@SupplierID", sup.SupplierID);
            updateCommand.Parameters.AddWithValue("@CompanyName", sup.CompanyName);
            updateCommand.Parameters.AddWithValue("@ContactName", sup.ContactName);
            updateCommand.Parameters.AddWithValue("@Address", sup.SuppliersAddress);
            updateCommand.Parameters.AddWithValue("@Phone", sup.SuppliersPhone);
            updateCommand.ExecuteNonQuery();
        }
        public Suppliers GetSupplierById(int id)
        {

            string sqlDelete = "SELECT * FROM Suppliers WHERE @SupplierID = SupplierID;";
            SqlCommand Command = new SqlCommand(sqlDelete, conn);
            Command.Parameters.AddWithValue("@SupplierID", id);
            using (var reader = Command.ExecuteReader())
            {
                if (reader.Read())
                {
                    sup = new Suppliers
                    {
                        CompanyName = reader["CompanyName"].ToString(),
                        ContactName = reader["ContactName"].ToString(),
                        SuppliersAddress = reader["Address"].ToString(),
                        SuppliersPhone = reader["Phone"].ToString()                        
                    };
                }
            }
            return sup;
        }
        public List<Suppliers> GetAllSupplier()
        {
            SqlCommand selectCommand = new SqlCommand("SELECT * FROM Suppliers ORDER BY SupplierID", conn);
            var listOfSuppliers = new List<Suppliers>();
            using (SqlDataReader reader = selectCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    var s = new Suppliers();
                    s.SupplierID = Convert.ToInt32(reader["SupplierID"].ToString());
                    s.CompanyName = reader["CompanyName"].ToString();
                    s.ContactName = reader["ContactName"].ToString();
                    s.SuppliersAddress = reader["Address"].ToString();
                    s.SuppliersPhone = reader["Phone"].ToString();                
                    listOfSuppliers.Add(s);
                }
            }
            return listOfSuppliers;

        }
        /****************************************************************Crud Product***********************************************/
        Products prod;
        public void AddProduct(Products p)
        {

            string sql = "INSERT INTO Products (SupplierID, ProductName, Quantity, CostPrice, UnitInStock, UnitOnOrders) VALUES "
                        + " (@SupplierID,@ProductName,@Quantity,@CostPrice,@UnitInStock,@UnitOnOrders)";
            SqlCommand insertCommand = new SqlCommand(sql, conn);
            insertCommand.Parameters.Add(new SqlParameter("@SupplierID", p.SupplierID));
            insertCommand.Parameters.Add(new SqlParameter("@ProductName", p.ProductName));
            insertCommand.Parameters.Add(new SqlParameter("@Quantity", p.Quantity));
            insertCommand.Parameters.Add(new SqlParameter("@CostPrice", p.CostPrice));
            insertCommand.Parameters.Add(new SqlParameter("@UnitInStock", p.UnitInStock));
            insertCommand.Parameters.Add(new SqlParameter("@UnitOnOrders", p.UnitInOrder));
            insertCommand.ExecuteNonQuery();
        }

        public void DeleteProduct(int Id)
        {
            string sqlDelete = "DELETE FROM Products WHERE @Id = Id;";
            SqlCommand deleteCommand = new SqlCommand(sqlDelete, conn);
            deleteCommand.Parameters.AddWithValue("@Id", Id);
            deleteCommand.ExecuteNonQuery();
        }

        public void UpdateProduct(Products p)
        {
            string sql = "UPDATE Products SET SupplierID=@SupplierID, ProductName=@ProductName, Quantity=@Quantity, CostPrice=@CostPrice, UnitInStock=@UnitInStock, UnitOnOrders=@UnitOnOrders  WHERE @ProductID= ProductID";
            SqlCommand updateCommand = new SqlCommand(sql, conn);
            updateCommand.Parameters.AddWithValue("@ProductID", p.ProductID);
            updateCommand.Parameters.Add(new SqlParameter("@SupplierID", p.SupplierID));
            updateCommand.Parameters.Add(new SqlParameter("@ProductName", p.ProductName));
            updateCommand.Parameters.Add(new SqlParameter("@Quantity", p.Quantity));
            updateCommand.Parameters.Add(new SqlParameter("@CostPrice", p.CostPrice));
            updateCommand.Parameters.Add(new SqlParameter("@UnitInStock", p.UnitInStock));
            updateCommand.Parameters.Add(new SqlParameter("@UnitOnOrders", p.UnitInOrder));
            updateCommand.ExecuteNonQuery();
        }
        public List<Products> GetAllProducts()
        {
            SqlCommand selectCommand = new SqlCommand("SELECT * FROM Products ORDER BY ProductID", conn);
            var listOfProducts = new List<Products>();
            using (SqlDataReader reader = selectCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    var p = new Products();
                    p.ProductID = Convert.ToInt32(reader["ProductID"].ToString());
                    p.ProductName = reader["ProductName"].ToString();
                    p.Quantity = Convert.ToInt64(reader["Quantity"].ToString());
                    p.CostPrice = float.Parse(reader["CostPrice"].ToString());
                    p.UnitInStock = Int32.Parse(reader["UnitInStock"].ToString());
                    p.UnitInOrder = Int32.Parse(reader["UnitOnOrders"].ToString());
                    listOfProducts.Add(p);
                }
            }
            return listOfProducts;

        }

        public Products GetProductsById(int id)
        {

            string sql = "SELECT * FROM Products WHERE @ProductID = ProductID;";
            SqlCommand Command = new SqlCommand(sql, conn);
            Command.Parameters.AddWithValue("@ProductID", id);
            using (var reader = Command.ExecuteReader())
            {
                if (reader.Read())
                {
                    prod = new Products
                    {
        
                        ProductName = reader["ProductName"].ToString(),
                        Quantity = Convert.ToInt64(reader["Quantity"].ToString()),
                        CostPrice = float.Parse(reader["Quantity"].ToString()),
                        UnitInStock = Int32.Parse(reader["Quantity"].ToString()),
                        UnitInOrder = Int32.Parse(reader["Quantity"].ToString())


                    };
                }
               
            }


            return prod;

        }

        /****************************************************************Crud Suppliers***********************************************/

        public void AddSuppliers(Suppliers s)
        {

            string sql = "INSERT INTO Suppliers (CompanyName, ContactName, Address, Phone) VALUES "
                        + " (@CompanyName,@ContactName,@Address,@Phone)";
            SqlCommand insertCommand = new SqlCommand(sql, conn);
            insertCommand.Parameters.Add(new SqlParameter("@CompanyName", s.CompanyName));
            insertCommand.Parameters.Add(new SqlParameter("@ContactName", s.ContactName));
            insertCommand.Parameters.Add(new SqlParameter("@Address", s.SuppliersAddress));
            insertCommand.Parameters.Add(new SqlParameter("@Phone", s.SuppliersPhone));
            insertCommand.ExecuteNonQuery();
        }

        public void DeleteSuppliers(int Id)
        {
            string sqlDelete = "DELETE FROM Suppliers WHERE @Id = Id;";
            SqlCommand deleteCommand = new SqlCommand(sqlDelete, conn);
            deleteCommand.Parameters.AddWithValue("@Id", Id);
            deleteCommand.ExecuteNonQuery();

        }

        public void UpdateSuppliers(Suppliers s)
        {
            string sql = "UPDATE Suppliers SET (CompanyName, ContactName, Address, CostPrice, Phone, UnitInOrder) VALUES (@CompanyName,@ContactName,@Address,@Phone)";
            SqlCommand updateCommand = new SqlCommand(sql, conn);
            updateCommand.Parameters.Add(new SqlParameter("@CompanyName", s.CompanyName));
            updateCommand.Parameters.Add(new SqlParameter("@ContactName", s.ContactName));
            updateCommand.Parameters.Add(new SqlParameter("@Address", s.SuppliersAddress));
            updateCommand.Parameters.Add(new SqlParameter("@Phone", s.SuppliersPhone));
            updateCommand.ExecuteNonQuery();

        }
        public List<Suppliers> GetAllSuppliers()
        {
            SqlCommand selectCommand = new SqlCommand("SELECT * FROM Suppliers ORDER BY SupplierID", conn);
            var listOfSuppliers = new List<Suppliers>();
            using (SqlDataReader reader = selectCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    var s = new Suppliers();
                    s.SupplierID = Convert.ToInt32(reader["SupplierID"].ToString());
                    s.CompanyName = reader["CompanyName"].ToString();
                    s.ContactName = reader["ContactName"].ToString();
                    s.SuppliersAddress = reader["Address"].ToString();
                    s.SuppliersPhone = reader["Phone"].ToString();
                    listOfSuppliers.Add(s);
                }
            }
            return listOfSuppliers;
        }
        /****************************************************************Crud Product***********************************************/
        Orders order;
        public void AddOrders(Orders o)
        {

            string sql = "INSERT INTO Orders (OrderDate, CustomerID, EmployeeID, Address, ProductID, SellingPrice, Quantity, Discount) VALUES "
                        + " (@OrderDate,@CustomerID,@EmployeeID,@Address,@ProductID,@SellingPrice,@Quantity,@Discount)";
            SqlCommand insertCommand = new SqlCommand(sql, conn);
            insertCommand.Parameters.Add(new SqlParameter("@CustomerID", o.CustomerID));
            insertCommand.Parameters.Add(new SqlParameter("@OrderDate", o.OrderDate));
            insertCommand.Parameters.Add(new SqlParameter("@EmployeeID", o.EmployeeID));
            insertCommand.Parameters.Add(new SqlParameter("@Address", o.Address));
            insertCommand.Parameters.Add(new SqlParameter("@ProductID", o.ProductID));
            insertCommand.Parameters.Add(new SqlParameter("@SellingPrice", o.SellingPrice));
            insertCommand.Parameters.Add(new SqlParameter("@Quantity", o.Quantity));
            insertCommand.Parameters.Add(new SqlParameter("@Discount", o.Discount));

            insertCommand.ExecuteNonQuery();
        }

        public void DeleteOrders(int Id)
        {
            string sqlDelete = "DELETE FROM Orders WHERE @Id = Id;";
            SqlCommand deleteCommand = new SqlCommand(sqlDelete, conn);
            deleteCommand.Parameters.AddWithValue("@Id", Id);
            deleteCommand.ExecuteNonQuery();
        }

        public void UpdateOrders(Orders p)
        {
            string sql = "UPDATE Orders SET OrderDate=@OrderDate, CustomerID=@CustomerID, EmployeeID=@EmployeeID, Address=@Address, ProductID=@ProductID, SellingPrice=@SellingPrice, Quantity=@Quantity, Discount=@Discount  WHERE @OrderID= OrderID";
            SqlCommand updateCommand = new SqlCommand(sql, conn);
            updateCommand.Parameters.AddWithValue("@OrderID", p.OrderID);
            updateCommand.Parameters.Add(new SqlParameter("@OrderDate", p.OrderDate));
            updateCommand.Parameters.Add(new SqlParameter("@CustomerID", p.CustomerID));
            updateCommand.Parameters.Add(new SqlParameter("@EmployeeID", p.EmployeeID));
            updateCommand.Parameters.Add(new SqlParameter("@Address", p.Address));
            updateCommand.Parameters.Add(new SqlParameter("@ProductID", p.ProductID));
            updateCommand.Parameters.Add(new SqlParameter("@SellingPrice", p.SellingPrice));
            updateCommand.Parameters.Add(new SqlParameter("@Quantity", p.Quantity));
            updateCommand.Parameters.Add(new SqlParameter("@Discount", p.Discount));

            updateCommand.ExecuteNonQuery();
        }
        public List<Orders> GetAllOrders()
        {
            SqlCommand selectCommand = new SqlCommand("SELECT * FROM Orders ORDER BY OrderID", conn);
            var listOfOrders = new List<Orders>();
            using (SqlDataReader reader = selectCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    var p = new Orders();
                    p.OrderID = Convert.ToInt32(reader["OrderID"].ToString());
                    p.CustomerID = Convert.ToInt32(reader["CustomerID"].ToString());
                    p.EmployeeID = Convert.ToInt32(reader["EmployeeID"].ToString());
                    p.OrderDate = (DateTime)reader["OrderDate"];
                    p.Address = reader["Address"].ToString();
                    p.ProductID= Convert.ToInt32(reader["ProductID"].ToString());
                    p.Quantity = Convert.ToInt32(reader["Quantity"].ToString());
                    p.SellingPrice = float.Parse(reader["SellingPrice"].ToString());
                    p.Discount = Convert.ToInt32(reader["Discount"].ToString());
                    listOfOrders.Add(p);
                }
            }
            return listOfOrders;

        }

        public Orders GetOrdersById(int id)
        {

            string sql = "SELECT * FROM Orders WHERE @OrderID = OrderID;";
            SqlCommand Command = new SqlCommand(sql, conn);
            Command.Parameters.AddWithValue("@OrderID", id);
            using (var reader = Command.ExecuteReader())
            {
                if (reader.Read())
                {
                    order = new Orders
                    {
                        CustomerID= Convert.ToInt32(reader["CustomerID"].ToString()),
                        EmployeeID = Convert.ToInt32(reader["EmployeeID"].ToString()),
                        OrderDate = (DateTime)reader["OrderDate"],
                        Address = reader["Address"].ToString(),
                       ProductID = Convert.ToInt32(reader["ProductID"].ToString()),
                       Quantity = Convert.ToInt32(reader["Quantity"].ToString()),
                       SellingPrice = float.Parse(reader["SellingPrice"].ToString()),
                        Discount = Convert.ToInt32(reader["Discount"].ToString())
                          //Total = float.Parse(reader["Total"].ToString())

                    };
                }

            }


            return order;

        }

        public Products GetProductByOrderIdProductId(int id)
        {

            string sql = "SELECT Products.ProductName, Products.CostPrice , Products.UnitInStock  FROM Products INNER JOIN  Orders On Products.ProductID= Orders.ProductID WHERE Orders.OrderID= @OrderID;";
            SqlCommand Command = new SqlCommand(sql, conn);
            Command.Parameters.AddWithValue("@OrderID", id);
            using (var reader = Command.ExecuteReader())
            {
                if (reader.Read())
                {
                    prod = new Products
                    {

                        ProductName = reader["ProductName"].ToString(),
                        Quantity = Convert.ToInt64(reader["Quantity"].ToString()),
                        CostPrice = float.Parse(reader["Quantity"].ToString()),
                        UnitInStock = Int32.Parse(reader["Quantity"].ToString()),
                        UnitInOrder = Int32.Parse(reader["Quantity"].ToString())
                      

                    };
                }

            }


            return prod;
        }

    }
}
