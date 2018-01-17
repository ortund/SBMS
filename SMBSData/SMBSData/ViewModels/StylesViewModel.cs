using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBMSData.ViewModels
{
    public class StylesViewModel
    {
        public List<StyleViewModel> Styles { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
