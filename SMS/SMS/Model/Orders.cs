using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model
{
    class Orders
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public int ProductID { get; set; }
        public float SellingPrice { get; set; }
        public int Quantity { get; set; }
        public float Discount { get; set; }
        public float Total
        {
            get
            {
                return Quantity * SellingPrice - Quantity * SellingPrice * Discount;
            }
           
        }
 
        private String _address;
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
    }
}
