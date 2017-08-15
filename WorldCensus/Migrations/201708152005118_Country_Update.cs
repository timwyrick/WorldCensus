namespace WorldCensus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Country_Update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Countries", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Countries", "Code");
        }
    }
}
