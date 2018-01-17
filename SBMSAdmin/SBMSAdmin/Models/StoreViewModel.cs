using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SBMSAdmin.Models
{
    public class StoreViewModel : SBMSData.ViewModels.StoreViewModel
    {
        public int PackageId { get; set; }
        public IEnumerable<SelectListItem> Packages { get; set; }
    }
}