namespace LoginRegister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Createdregister : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        MiddleName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        FathersName = c.String(nullable: false),
                        MobileNumber = c.String(nullable: false, maxLength: 10),
                        Email = c.String(nullable: false),
                        AadharNumber = c.String(nullable: false, maxLength: 12),
                        DateofBirth = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Occupation = c.String(nullable: false),
                        AnnualIncome = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
