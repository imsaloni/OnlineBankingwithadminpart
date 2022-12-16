namespace LoginRegister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class accountDetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountDetails",
                c => new
                    {
                        AccountNumber = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.AccountNumber)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
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
            
            CreateTable(
                "dbo.Transaction",
                c => new
                    {
                        TransationId = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                        payeeAccountNo = c.Int(nullable: false),
                        TransationAmount = c.String(nullable: false),
                        TransactionType = c.String(),
                        TransactionDate = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.TransationId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transaction", "UserId", "dbo.Users");
            DropForeignKey("dbo.AccountDetails", "UserId", "dbo.Users");
            DropIndex("dbo.Transaction", new[] { "UserId" });
            DropIndex("dbo.AccountDetails", new[] { "UserId" });
            DropTable("dbo.Transaction");
            DropTable("dbo.Users");
            DropTable("dbo.AccountDetails");
        }
    }
}
