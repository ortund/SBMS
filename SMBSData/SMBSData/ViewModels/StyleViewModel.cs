using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBMSData.ViewModels
{
    public class StyleViewModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CompletionTime { get; set; }
        public decimal Commission { get; set; }
        public byte[] Image { get; set; }
        public string ImageType { get; set; }

        public StoreViewModel Store { get; set; }
        public EmployeeViewModel Employee { get; set; }
    }
}
