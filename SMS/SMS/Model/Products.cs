using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS
{
    class Products
    {
        public int ProductID { get; set; }
        public int SupplierID { get; set; }
        public string ProductName { get; set; }
        public long Quantity { get; set; }
        public float CostPrice { get; set; }
        public int UnitInStock { get; set; }
        public int UnitInOrder { get; set; }
        //UnitinStock > Unitinorder
    }
}
