using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBMSData.ViewModels
{
    public class AdvertViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Media { get; set; }
        public string MediaType { get; set; }
        public string URL { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsSupplier { get; set; }
        public bool IsStore { get; set; }
        public bool IsPublic { get; set; }
    }
}
