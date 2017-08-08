namespace WorldCensus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Continents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Population = c.Int(nullable: false),
                        Continent_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Continents", t => t.Continent_ID)
                .Index(t => t.Continent_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Countries", "Continent_ID", "dbo.Continents");
            DropIndex("dbo.Countries", new[] { "Continent_ID" });
            DropTable("dbo.Countries");
            DropTable("dbo.Continents");
        }
    }
}
