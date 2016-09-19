namespace BitBookWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserInformationsIsAddDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BasicInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProfilePhotoUrl = c.String(),
                        CoverPhotoUrl = c.String(),
                        AreaOfInterest = c.String(),
                        Location = c.String(),
                        Experience = c.String(),
                        Education = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BasicInfoes");
        }
    }
}
