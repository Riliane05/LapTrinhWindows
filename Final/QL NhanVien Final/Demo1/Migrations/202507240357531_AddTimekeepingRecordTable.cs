namespace Demo1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTimekeepingRecordTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TimekeepingRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        CheckInTime = c.DateTime(),
                        CheckOutTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimekeepingRecords", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.TimekeepingRecords", new[] { "EmployeeId" });
            DropTable("dbo.TimekeepingRecords");
        }
    }
}
