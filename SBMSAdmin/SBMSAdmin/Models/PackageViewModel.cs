using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SBMSAdmin.Models
{
    public class PackageViewModel : SBMSData.ViewModels.PackageViewModel
    {
        public IEnumerable<SelectListItem> Features { get; set; }
        public int[] SelectedFeatures { get; set; }
    }
}