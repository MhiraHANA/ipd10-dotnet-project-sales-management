﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SMS
{
    class Employees
    {
        public int EmployeeID { get; set; }
        public String LastName { get; set; }
        public String FirstName { get; set; }
        public DateTime HireDate { get; set; }
        public String Address { get; set; }

        public String Phone { get; set; }

        public Byte[] Photo { get; set; }
     //   public Image PhotoUser { get; set; }

        private String _username;
        private String _password;
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        public String UserName {
            get { return _username; }
            set
            {
                if (value.Length < 2 || value.Length > 50)
                {
                    throw new ArgumentOutOfRangeException("UserName must be between 2 and 50 characters long");
                }
                _username = value;
            }
        }
        public String Password
        {
            get { return _password; }
            set
            {
                if (value.Length < 2 || value.Length > 50)
                {
                    throw new ArgumentOutOfRangeException("UserName must be between 2 and 50 characters long");
                }
                _password = value;
            }
        }
    }
}
