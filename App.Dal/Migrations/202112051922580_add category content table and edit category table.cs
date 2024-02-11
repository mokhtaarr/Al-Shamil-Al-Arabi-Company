namespace App.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcategorycontenttableandeditcategorytable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoryCnotents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArContent = c.String(),
                        EnContent = c.String(),
                        Images = c.String(),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            DropColumn("dbo.Categories", "ArContent");
            DropColumn("dbo.Categories", "EnContent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "EnContent", c => c.String());
            AddColumn("dbo.Categories", "ArContent", c => c.String());
            DropForeignKey("dbo.CategoryCnotents", "CategoryID", "dbo.Categories");
            DropIndex("dbo.CategoryCnotents", new[] { "CategoryID" });
            DropTable("dbo.CategoryCnotents");
        }
    }
}
