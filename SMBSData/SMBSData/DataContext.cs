using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBMSData
{
    public class DataContext : DbContext
    {
        public DbSet<Advert> Adverts { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeStyleMetrics> EmployeeStyleMetrics { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<PackageFeatures> PackageFeatures { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Style> Styles { get; set; }
        public DbSet<StyleMaterials> StyleMaterials { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public DataContext()
            : base("DefaultConnection")
        {

        }
    }
}
