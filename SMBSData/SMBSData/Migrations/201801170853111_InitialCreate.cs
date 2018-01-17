namespace SMBSData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adverts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Media = c.Binary(),
                        MediaType = c.String(),
                        URL = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsSupplier = c.Boolean(nullable: false),
                        IsStore = c.Boolean(nullable: false),
                        IsPublic = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        WaitNumber = c.String(),
                        Status = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Commission = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Deleted = c.Boolean(nullable: false),
                        Store_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stores", t => t.Store_Id)
                .Index(t => t.Store_Id);
            
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Telephone = c.String(),
                        ContactPerson = c.String(),
                        ContactNumber = c.String(),
                        EmailAddress = c.String(),
                        Terminals = c.Int(nullable: false),
                        Commission = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsActive = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Package_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Packages", t => t.Package_Id)
                .Index(t => t.Package_Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        Address = c.String(),
                        Telephone = c.String(),
                        Mobile = c.String(),
                        Position = c.String(),
                        IsOnDuty = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Store_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stores", t => t.Store_Id)
                .Index(t => t.Store_Id);
            
            CreateTable(
                "dbo.Packages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Styles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CompletionTime = c.Int(nullable: false),
                        Commission = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Image = c.Binary(),
                        ImageType = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        Employee_Id = c.Int(),
                        Store_Id = c.Int(),
                        Appointment_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Employee_Id)
                .ForeignKey("dbo.Stores", t => t.Store_Id)
                .ForeignKey("dbo.Appointments", t => t.Appointment_Id)
                .Index(t => t.Employee_Id)
                .Index(t => t.Store_Id)
                .Index(t => t.Appointment_Id);
            
            CreateTable(
                "dbo.EmployeeStyleMetrics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MetricDate = c.DateTime(nullable: false),
                        Duration = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Employee_Id = c.Int(),
                        Style_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Employee_Id)
                .ForeignKey("dbo.Styles", t => t.Style_Id)
                .Index(t => t.Employee_Id)
                .Index(t => t.Style_Id);
            
            CreateTable(
                "dbo.Features",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Number = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Document = c.Binary(),
                        DocumentType = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        Store_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stores", t => t.Store_Id)
                .Index(t => t.Store_Id);
            
            CreateTable(
                "dbo.Materials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Quantity = c.Int(nullable: false),
                        Units = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        Store_Id = c.Int(),
                        Supplier_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stores", t => t.Store_Id)
                .ForeignKey("dbo.Suppliers", t => t.Supplier_Id)
                .Index(t => t.Store_Id)
                .Index(t => t.Supplier_Id);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PackageFeatures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Deleted = c.Boolean(nullable: false),
                        Feature_Id = c.Int(),
                        Package_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Features", t => t.Feature_Id)
                .ForeignKey("dbo.Packages", t => t.Package_Id)
                .Index(t => t.Feature_Id)
                .Index(t => t.Package_Id);
            
            CreateTable(
                "dbo.Shifts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Comments = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        Employee_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Employee_Id)
                .Index(t => t.Employee_Id);
            
            CreateTable(
                "dbo.StyleMaterials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Unit = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        Material_Id = c.Int(),
                        Style_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Materials", t => t.Material_Id)
                .ForeignKey("dbo.Styles", t => t.Style_Id)
                .Index(t => t.Material_Id)
                .Index(t => t.Style_Id);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Deleted = c.Boolean(nullable: false),
                        Store_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stores", t => t.Store_Id)
                .Index(t => t.Store_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "Store_Id", "dbo.Stores");
            DropForeignKey("dbo.StyleMaterials", "Style_Id", "dbo.Styles");
            DropForeignKey("dbo.StyleMaterials", "Material_Id", "dbo.Materials");
            DropForeignKey("dbo.Shifts", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.PackageFeatures", "Package_Id", "dbo.Packages");
            DropForeignKey("dbo.PackageFeatures", "Feature_Id", "dbo.Features");
            DropForeignKey("dbo.Materials", "Supplier_Id", "dbo.Suppliers");
            DropForeignKey("dbo.Materials", "Store_Id", "dbo.Stores");
            DropForeignKey("dbo.Invoices", "Store_Id", "dbo.Stores");
            DropForeignKey("dbo.EmployeeStyleMetrics", "Style_Id", "dbo.Styles");
            DropForeignKey("dbo.EmployeeStyleMetrics", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.Styles", "Appointment_Id", "dbo.Appointments");
            DropForeignKey("dbo.Styles", "Store_Id", "dbo.Stores");
            DropForeignKey("dbo.Styles", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.Appointments", "Store_Id", "dbo.Stores");
            DropForeignKey("dbo.Stores", "Package_Id", "dbo.Packages");
            DropForeignKey("dbo.Employees", "Store_Id", "dbo.Stores");
            DropIndex("dbo.Transactions", new[] { "Store_Id" });
            DropIndex("dbo.StyleMaterials", new[] { "Style_Id" });
            DropIndex("dbo.StyleMaterials", new[] { "Material_Id" });
            DropIndex("dbo.Shifts", new[] { "Employee_Id" });
            DropIndex("dbo.PackageFeatures", new[] { "Package_Id" });
            DropIndex("dbo.PackageFeatures", new[] { "Feature_Id" });
            DropIndex("dbo.Materials", new[] { "Supplier_Id" });
            DropIndex("dbo.Materials", new[] { "Store_Id" });
            DropIndex("dbo.Invoices", new[] { "Store_Id" });
            DropIndex("dbo.EmployeeStyleMetrics", new[] { "Style_Id" });
            DropIndex("dbo.EmployeeStyleMetrics", new[] { "Employee_Id" });
            DropIndex("dbo.Styles", new[] { "Appointment_Id" });
            DropIndex("dbo.Styles", new[] { "Store_Id" });
            DropIndex("dbo.Styles", new[] { "Employee_Id" });
            DropIndex("dbo.Employees", new[] { "Store_Id" });
            DropIndex("dbo.Stores", new[] { "Package_Id" });
            DropIndex("dbo.Appointments", new[] { "Store_Id" });
            DropTable("dbo.Transactions");
            DropTable("dbo.StyleMaterials");
            DropTable("dbo.Shifts");
            DropTable("dbo.PackageFeatures");
            DropTable("dbo.Suppliers");
            DropTable("dbo.Materials");
            DropTable("dbo.Invoices");
            DropTable("dbo.Features");
            DropTable("dbo.EmployeeStyleMetrics");
            DropTable("dbo.Styles");
            DropTable("dbo.Packages");
            DropTable("dbo.Employees");
            DropTable("dbo.Stores");
            DropTable("dbo.Appointments");
            DropTable("dbo.Adverts");
        }
    }
}
