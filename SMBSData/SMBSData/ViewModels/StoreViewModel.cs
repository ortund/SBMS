using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBMSData.ViewModels
{
    public class StoreViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public int Terminals { get; set; }
        public decimal Commission { get; set; }

        public virtual ICollection<EmployeeViewModel> Employees { get; set; }
        public virtual PackageViewModel Package { get; set; }

        public StoreViewModel()
        {
            this.Employees = new HashSet<EmployeeViewModel>();
        }
    }
}
