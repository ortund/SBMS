using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBMSData.ViewModels
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string WaitNumber { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }
        public decimal Commission { get; set; }
        public List<StyleViewModel> Styles { get; set; }
        public StoreViewModel Store { get; set; }
    }
}
