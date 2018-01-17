using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBMSData
{
    public class EmployeeStyleMetrics : TableMeta
    {
        public DateTime MetricDate { get; set; }
        public int Duration { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Style Style { get; set; }
    }
}
