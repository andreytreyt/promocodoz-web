namespace Promocodoz.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveNullableFromPlatform : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Codes", "Platform", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Codes", "Platform", c => c.Int());
        }
    }
}
