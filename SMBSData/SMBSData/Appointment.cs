using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBMSData
{
    public class Appointment : TableMeta
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string WaitNumber { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }
        public decimal Commission { get; set; }
        public virtual ICollection<Style> Styles { get; set; }
        public virtual Store Store { get; set; }
    }
}
