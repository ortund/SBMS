using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBMSData
{
    public class Invoice : TableMeta
    {
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public decimal Amount { get; set; }
        public bool Paid { get; set; }
        public DateTime? DatePaid { get; set; }

        public virtual Store Store { get; set; }
    }
}
