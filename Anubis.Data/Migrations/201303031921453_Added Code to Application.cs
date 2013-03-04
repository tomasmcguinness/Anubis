namespace Anubis.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCodetoApplication : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applications", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Applications", "Code");
        }
    }
}
