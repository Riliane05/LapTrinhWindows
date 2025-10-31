namespace Demo1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModel : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Employees", name: "Department_Id", newName: "PhongBanID");
            RenameIndex(table: "dbo.Employees", name: "IX_Department_Id", newName: "IX_PhongBanID");
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50),
                        PassWord = c.String(nullable: false, maxLength: 100),
                        AccountType = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Departments", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Employees", "PhongBan");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "PhongBan", c => c.String());
            AlterColumn("dbo.Departments", "Name", c => c.String());
            DropTable("dbo.Users");
            RenameIndex(table: "dbo.Employees", name: "IX_PhongBanID", newName: "IX_Department_Id");
            RenameColumn(table: "dbo.Employees", name: "PhongBanID", newName: "Department_Id");
        }
    }
}
