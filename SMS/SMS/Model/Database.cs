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
          //  insertCommand.Parameters.Add(new SqlParameter("@Phone", emp.Phone));
            insertCommand.Parameters.Add(new SqlParameter("@Photo", emp.Photo));
            insertCommand.Parameters.Add(new SqlParameter("@UserName", emp.UserName));
            insertCommand.Parameters.Add(new SqlParameter("@Password", emp.Password));
            insertCommand.ExecuteNonQuery();

        }

        public void DeleteEmployee(int Id)
        {
            string sqlDelete = "DELETE FROM Employees WHERE Id =Id;";
            SqlCommand deleteCommand = new SqlCommand(sqlDelete, conn);
            //deleteCommand.ExecuteNonQuery();
        }

        public void UpdateEmployee(Employees emp)
        {
            string sql = "UPDATE Person(LastName, FirstName, HireDate, Address, Photo, UserName, Password) VALUES (@LastNameName,@FirstName,@HireDate,@Address,@Photo,@UserName,@Password)";
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
    }

}
