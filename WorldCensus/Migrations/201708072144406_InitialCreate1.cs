namespace WorldCensus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Countries", "FoundedDate", c => c.Int(nullable: false));
            AddColumn("dbo.Countries", "EndDdate", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Countries", "EndDdate");
            DropColumn("dbo.Countries", "FoundedDate");
        }
    }
}
