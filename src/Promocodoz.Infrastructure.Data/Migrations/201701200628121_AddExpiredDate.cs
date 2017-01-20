namespace Promocodoz.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExpiredDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Codes", "ExpiredDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Codes", "ExpiredDate");
        }
    }
}
