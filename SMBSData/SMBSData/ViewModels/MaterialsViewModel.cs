using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBMSData.ViewModels
{
    public class MaterialsViewModel
    {
        public List<MaterialViewModel> Materials { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
