namespace LoginRegister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class admin : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "UserStatus", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "UserStatus", c => c.String(nullable: false));
        }
    }
}
