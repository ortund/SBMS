using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBMSData.ViewModels
{
    public class MaterialViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Units { get; set; }

        public StoreViewModel Store { get; set; }
        public SupplierViewModel Supplier { get; set; }
    }
}
