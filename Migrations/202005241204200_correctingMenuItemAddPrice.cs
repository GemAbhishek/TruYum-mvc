namespace TruYum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correctingMenuItemAddPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MenuItem", "Price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MenuItem", "Price");
        }
    }
}
