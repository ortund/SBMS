namespace SBMSAdmin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedInvoicesModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "Paid", c => c.Boolean(nullable: false));
            AddColumn("dbo.Invoices", "DatePaid", c => c.DateTime());
            DropColumn("dbo.Invoices", "Document");
            DropColumn("dbo.Invoices", "DocumentType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invoices", "DocumentType", c => c.String());
            AddColumn("dbo.Invoices", "Document", c => c.Binary());
            DropColumn("dbo.Invoices", "DatePaid");
            DropColumn("dbo.Invoices", "Paid");
        }
    }
}
