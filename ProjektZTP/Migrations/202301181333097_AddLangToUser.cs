namespace ProjektZTP.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddLangToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Lang", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Lang");
        }
    }
}