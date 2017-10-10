using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model
{
    class Suppliers
    {
        public int SupplierID { get; set; }
        public String CompanyName { get; set; }
        public String ContactName { get; set; }
        public String SuppliersAddress { get; set; }
        public String SuppliersPhone { get; set; }
        //List<ComboData> _typeList = new List<ComboData>();

        //public List<ComboData> TypeList
        //{
        //    get { return _typeList; }
        //    set
        //    {
        //        _typeList = value;
        //        RaisePropertyChanged(() => TypeList);
        //    }
        //}

        //private void RaisePropertyChanged(Func<List<ComboData>> p)
        //{
        //    throw new NotImplementedException();
        //}

        //List<ComboData> _statusList = new List<ComboData>();

        //public List<ComboData> StatusList
        //{
        //    get { return _statusList; }
        //    set
        //    {
        //        _statusList = value;
        //        RaisePropertyChanged(() => StatusList);
        //    }
        //}

    }
}
