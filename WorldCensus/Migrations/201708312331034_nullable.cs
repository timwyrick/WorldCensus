namespace WorldCensus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Countries", "LifeExpectancy", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Countries", "LifeExpectancy", c => c.Double(nullable: false));
        }
    }
}
