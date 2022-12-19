namespace LoginRegister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changesinuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserStatus", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "UserStatus");
        }
    }
}
