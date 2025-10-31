namespace Demo1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAvatarToEmployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Avatar", c => c.Binary());
            AddColumn("dbo.Users", "TenTK", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Users", "MatKhau", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Users", "LoaiTK", c => c.Boolean(nullable: false));
            DropColumn("dbo.Users", "UserName");
            DropColumn("dbo.Users", "PassWord");
            DropColumn("dbo.Users", "AccountType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "AccountType", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "PassWord", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Users", "UserName", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Users", "LoaiTK");
            DropColumn("dbo.Users", "MatKhau");
            DropColumn("dbo.Users", "TenTK");
            DropColumn("dbo.Employees", "Avatar");
        }
    }
}
