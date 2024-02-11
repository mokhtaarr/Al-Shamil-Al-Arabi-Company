namespace App.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intialDatabase3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Sort", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "Sort");
        }
    }
}
