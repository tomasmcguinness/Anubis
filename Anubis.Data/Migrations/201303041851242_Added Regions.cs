namespace Anubis.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRegions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationRegions",
                c => new
                    {
                        ApplicationRegionId = c.Long(nullable: false, identity: true),
                        RegionId = c.Int(nullable: false),
                        Application_ApplicationId = c.Long(),
                    })
                .PrimaryKey(t => t.ApplicationRegionId)
                .ForeignKey("dbo.Applications", t => t.Application_ApplicationId)
                .Index(t => t.Application_ApplicationId);
            
            AddColumn("dbo.Applications", "OwnerId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationRegions", "Application_ApplicationId", "dbo.Applications");
            DropIndex("dbo.ApplicationRegions", new[] { "Application_ApplicationId" });
            DropColumn("dbo.Applications", "OwnerId");
            DropTable("dbo.ApplicationRegions");
        }
    }
}
