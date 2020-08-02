namespace Marketplace.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class auctionValidations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Auctions", "Title", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Auctions", "StartTime", c => c.DateTime());
            AlterColumn("dbo.Auctions", "EndTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Auctions", "EndTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Auctions", "StartTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Auctions", "Title", c => c.String());
        }
    }
}
