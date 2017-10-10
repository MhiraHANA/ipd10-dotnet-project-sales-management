﻿using System;
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
        /****************************************************************Crud Employee***********************************************/

        public void AddEmployee(Employees emp)
        {

            string sql = "INSERT INTO Employees (LastName, FirstName, HireDate, Address, Photo, UserName, Password) VALUES "
                        + " (@LastName,@FirstName,@HireDate,@Address,@Photo,@UserName,@Password)";
            SqlCommand insertCommand = new SqlCommand(sql, conn);

            insertCommand.Parameters.Add(new SqlParameter("@LastName",  emp.LastName));
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
            string sql = "UPDATE Employees SET (LastName, FirstName, HireDate, Address, Photo, UserName, Password) VALUES (@LastNameName,@FirstName,@HireDate,@Address,@Photo,@UserName,@Password)";
            SqlCommand updateCommand = new SqlCommand(sql, conn);
            updateCommand.Parameters.Add(new SqlParameter("@LastName", emp.LastName));
            updateCommand.Parameters.Add(new SqlParameter("@FirstName", emp.FirstName));
            updateCommand.Parameters.Add(new SqlParameter("@HireDate", emp.HireDate));
            updateCommand.Parameters.Add(new SqlParameter("@Address", emp.Address));
            updateCommand.Parameters.Add(new SqlParameter("@Photo", emp.Photo));
            updateCommand.Parameters.Add(new SqlParameter("@UserName", emp.UserName));
            updateCommand.Parameters.Add(new SqlParameter("@Password", emp.Password));
            updateCommand.ExecuteNonQuery();

        }

        /****************************************************************Crud Product***********************************************/

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
            string sql = "UPDATE Products SET (SupplierID, ProductName, Quantity, CostPrice, UnitInStock, UnitOnOrders) VALUES (@SupplierID,@ProductName,@Quantity,@CostPrice,@UnitInStock,@UnitOnOrders)";
            SqlCommand updateCommand = new SqlCommand(sql, conn);
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
                    p.Quantity= Convert.ToInt64(reader["Quantity"].ToString());
                    p.CostPrice = float.Parse(reader["CostPrice"].ToString());
                    p.UnitInStock = Int32.Parse(reader["UnitInStock"].ToString());
                    p.UnitInOrder = Int32.Parse(reader["UnitOnOrders"].ToString());
                   
                    listOfProducts.Add(p);

                }
            }
            return listOfProducts;

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
       

        }
  
    

    }
