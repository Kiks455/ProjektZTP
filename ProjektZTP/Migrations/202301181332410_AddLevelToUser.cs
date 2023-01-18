namespace ProjektZTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLevelToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Level", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Level");
        }
    }
}
