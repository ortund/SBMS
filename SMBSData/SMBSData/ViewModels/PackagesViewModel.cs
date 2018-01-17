using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBMSData.ViewModels
{
    public class PackagesViewModel
    {
        public List<PackageViewModel> Packages { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
