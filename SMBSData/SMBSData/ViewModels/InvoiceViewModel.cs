using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBMSData.ViewModels
{
    public class InvoiceViewModel
    {
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public decimal Amount { get; set; }
        public byte[] Document { get; set; }
        public string DocumentType { get; set; }

        public virtual Store Store { get; set; }
    }
}
