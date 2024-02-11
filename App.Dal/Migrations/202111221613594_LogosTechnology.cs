namespace App.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LogosTechnology : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogosTechnologies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArTitel = c.String(),
                        EnTitel = c.String(),
                        ArDescription = c.String(),
                        EnDescription = c.String(),
                        Image = c.String(),
                        Sort = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LogosTechnologies");
        }
    }
}
