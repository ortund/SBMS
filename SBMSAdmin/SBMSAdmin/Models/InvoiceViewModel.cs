using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SBMSAdmin.Models
{
    public class InvoiceViewModel : SBMSData.ViewModels.InvoiceViewModel
    {
        public int StoreId { get; set; }
        public IEnumerable<SelectListItem> Stores { get; set; }
        public string DocumentFileName { get; set; }
    }
}