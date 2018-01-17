using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBMSData
{
    public class Transaction : TableMeta
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }

        public virtual Store Store { get; set; }
    }
}
