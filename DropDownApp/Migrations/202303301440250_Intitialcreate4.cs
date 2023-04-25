namespace DropDownApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Intitialcreate4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.sampleEmps",
                c => new
                    {
                        EmpId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.EmpId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.sampleEmps");
        }
    }
}
