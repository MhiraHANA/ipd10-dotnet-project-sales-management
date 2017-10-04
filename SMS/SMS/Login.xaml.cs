﻿using System;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ipd\Documents\SMSDB.mdf;Integrated Security=True;Connect Timeout=30");
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                string query = "SELECT COUNT(1) FROM Employees WHERE Username=@Username and Password=@Password";
                SqlCommand sqlcmd = new SqlCommand(query, conn);
                sqlcmd.CommandType = CommandType.Text;
                sqlcmd.Parameters.AddWithValue("@Username", tbUsername.Text);
                sqlcmd.Parameters.AddWithValue("@Password", tbPassword.Password);
                int count = Convert.ToInt32(sqlcmd.ExecuteScalar());
                if (count == 1)
                {
                    MainWindow dashboard = new MainWindow();
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Username or Password is incorrect.", "SMS Application", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbUsername.Text = "";
            tbPassword.Password = "";
        }
    }
}
