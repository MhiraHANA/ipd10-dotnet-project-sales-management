using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model
{
    class Customers
    {
        private String _companyname;
        private String _address;
        private String _phone;
        private String _email;

        public int CustomerID { get; set; }
        public string CompanyName
        {
            get { return _companyname; }
            set
            {
                if (value.Length < 2 || value.Length > 50)
                {
                    throw new ArgumentOutOfRangeException("Company Name must be between 2 and 50 characters long");
                }
                _companyname = value;
            }
        }
        public String Address
        {
            get { return _address; }
            set
            {
                if (value.Length < 1 || value.Length > 150)
                {
                    throw new ArgumentOutOfRangeException("Address must be between 1 and 150 characters long.");
                }
                _address = value;
            }
        } 

        public String Phone
        {
            get { return _phone; }
            set
            {
                if (value.Length <10 || value.Length > 20)
                {
                    throw new ArgumentOutOfRangeException("Phone must be between 1 and 150 characters long.");
                }
                _phone = value;
            }
        }

        public String Email
        {
            get { return _email; }
            set
            {
                if (value.Length < 10 || value.Length > 150)
                {
                    throw new ArgumentOutOfRangeException("Email must be between 1 and 150 characters long.");
                }
                _email = value;
            }
        }
    }
}
