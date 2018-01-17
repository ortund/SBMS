using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBMSData
{
    public class StyleMaterials : TableMeta
    {
        public int Quantity { get; set; } // Defines quantity of the material required by the style.
        public string Unit { get; set; }
        public virtual Material Material { get; set; }
        public virtual Style Style { get; set; }
    }
}
