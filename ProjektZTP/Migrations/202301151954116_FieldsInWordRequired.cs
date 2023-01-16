namespace ProjektZTP.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class FieldsInWordRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Words", "WordEn", c => c.String(nullable: false));
            AlterColumn("dbo.Words", "WordPl", c => c.String(nullable: false));
        }

        public override void Down()
        {
            AlterColumn("dbo.Words", "WordPl", c => c.String());
            AlterColumn("dbo.Words", "WordEn", c => c.String());
        }
    }
}