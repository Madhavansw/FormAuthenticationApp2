namespace DropDownApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialPasswordLogOnCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.sampleEmps", "Password", c => c.String());
            AddColumn("dbo.sampleEmps", "LogOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.sampleEmps", "LogOn");
            DropColumn("dbo.sampleEmps", "Password");
        }
    }
}
