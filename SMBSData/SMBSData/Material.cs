using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBMSData
{
    public class Material : TableMeta
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Units { get; set; }

        public virtual Store Store { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
