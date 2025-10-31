namespace Demo1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTimekeepingToEmployee : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Timekeepings", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Timekeepings", new[] { "EmployeeId" });
            AddColumn("dbo.Employees", "CheckInTime", c => c.DateTime());
            AddColumn("dbo.Employees", "CheckOutTime", c => c.DateTime());
            DropTable("dbo.Timekeepings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Timekeepings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        CheckIn = c.Time(precision: 7),
                        CheckOut = c.Time(precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Employees", "CheckOutTime");
            DropColumn("dbo.Employees", "CheckInTime");
            CreateIndex("dbo.Timekeepings", "EmployeeId");
            AddForeignKey("dbo.Timekeepings", "EmployeeId", "dbo.Employees", "Id", cascadeDelete: true);
        }
    }
}
