using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBMSData
{
    public class PackageFeatures : TableMeta
    {
        public virtual Package Package { get; set; }
        public virtual Feature Feature { get; set; }
    }
}
