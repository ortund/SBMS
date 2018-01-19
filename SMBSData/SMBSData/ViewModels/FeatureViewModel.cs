using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBMSData.ViewModels
{
    public class FeatureViewModel
    {
        public int Id { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}
