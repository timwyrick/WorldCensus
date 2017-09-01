namespace WorldCensus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AreaAndGDP : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Countries", "TotalArea", c => c.Double());
            AddColumn("dbo.Countries", "GDP", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Countries", "GDP");
            DropColumn("dbo.Countries", "TotalArea");
        }
    }
}
