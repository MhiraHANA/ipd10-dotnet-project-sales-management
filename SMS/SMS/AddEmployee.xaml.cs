﻿using Microsoft.Win32;
using SMS.Model;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace SMS
{
    /// <summary>
    /// Interaction logic for AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        Database db = new Database();
        public AddEmployee()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Employees emp = new Employees();
            emp.FirstName = tbFirstName.Text;
            emp.LastName = tbLastName.Text;
            emp.HireDate = DateTime.Parse( tbHireDate.Text);
            emp.Address = tbAddress.Text;
            emp.UserName = tbUserName.Text;
            emp.Password = tbPassword.Text;
            emp.Phone = tbPhone.Text;
            emp.Photo = ConvertImageToBinary(tbNameImage.Text);
            db.AddEmployee(emp);
            MessageBox.Show("Employee has been added succefully!", "Confirm dialog", MessageBoxButton.OK, MessageBoxImage.Information);
            tbFirstName.Clear();
            tbLastName.Clear();
            //tbHireDate.ClearValue();
            tbAddress.Clear();
            tbUserName.Clear();
            tbPassword.Clear();
            tbPhone.Clear();
            tbNameImage.Clear();
          
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

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                imageEmployee.Source = new BitmapImage(new Uri(op.FileName));
                string fileName = op.FileName;

                tbNameImage.Text = fileName;
                }


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

        
    }
}
