namespace LoginRegister.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccountStatement : DbMigration
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
                        UserStatus = c.String(),
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
                "dbo.Admin",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transaction",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        AccountNumber = c.Int(nullable: false),
                        payeeAccountNo = c.Int(nullable: false),
                        TransationAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TransactionType = c.String(),
                        TransactionDate = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountDetails", t => t.AccountNumber, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.AccountNumber);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transaction", "UserId", "dbo.Users");
            DropForeignKey("dbo.Transaction", "AccountNumber", "dbo.AccountDetails");
            DropForeignKey("dbo.AccountDetails", "UserId", "dbo.Users");
            DropIndex("dbo.Transaction", new[] { "AccountNumber" });
            DropIndex("dbo.Transaction", new[] { "UserId" });
            DropIndex("dbo.AccountDetails", new[] { "UserId" });
            DropTable("dbo.Transaction");
            DropTable("dbo.Admin");
            DropTable("dbo.Users");
            DropTable("dbo.AccountDetails");
        }
    }
}
