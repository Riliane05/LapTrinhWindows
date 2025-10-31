namespace Demo1.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Demo1.DemoDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Demo1.DemoDbContext context)
        {
            context.Users.AddOrUpdate(
                u => u.TenTK,
                new User { TenTK = "duongthihoaianh", MatKhau = "16022005", LoaiTK = true },
                new User { TenTK = "nhanvien1", MatKhau = "12345678", LoaiTK = false }
            );
        }
    }
}
