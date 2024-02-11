namespace App.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intialDatabase12 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Sort", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "Sort", c => c.Int(nullable: false));
        }
    }
}
