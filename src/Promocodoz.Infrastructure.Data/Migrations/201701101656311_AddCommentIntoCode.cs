namespace Promocodoz.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCommentIntoCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Codes", "Comment", c => c.String(maxLength: 32));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Codes", "Comment");
        }
    }
}
