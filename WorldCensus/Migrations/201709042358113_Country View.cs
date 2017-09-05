namespace WorldCensus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CountryView : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Countries", "Synopsis", c => c.String());
            AddColumn("dbo.Countries", "FilePath1", c => c.String());
            AddColumn("dbo.Countries", "FilePath2", c => c.String());
            AddColumn("dbo.Countries", "FilePath3", c => c.String());
            AddColumn("dbo.Countries", "EndDate", c => c.Int(nullable: false));
            DropColumn("dbo.Countries", "EndDdate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Countries", "EndDdate", c => c.Int(nullable: false));
            DropColumn("dbo.Countries", "EndDate");
            DropColumn("dbo.Countries", "FilePath3");
            DropColumn("dbo.Countries", "FilePath2");
            DropColumn("dbo.Countries", "FilePath1");
            DropColumn("dbo.Countries", "Synopsis");
        }
    }
}
