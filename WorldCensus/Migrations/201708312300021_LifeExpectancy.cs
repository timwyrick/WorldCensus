namespace WorldCensus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LifeExpectancy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Countries", "LifeExpectancy", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Countries", "LifeExpectancy");
        }
    }
}
