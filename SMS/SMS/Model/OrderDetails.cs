using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model
{
    class OrderDetails
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public float SellingPrice { get; set; }
        public int Quantity { get; set; }
        public int Discount{ get; set; }
    }
}
