namespace DropDownApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreateModifiedEmpSAmple : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.sampleEmps", "IsTrained", c => c.Boolean(nullable: false));
            AddColumn("dbo.sampleEmps", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.sampleEmps", "Roles", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.sampleEmps", "Roles");
            DropColumn("dbo.sampleEmps", "IsActive");
            DropColumn("dbo.sampleEmps", "IsTrained");
        }
    }
}
