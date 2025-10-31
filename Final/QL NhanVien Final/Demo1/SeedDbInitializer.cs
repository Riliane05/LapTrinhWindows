using System.Data.Entity;

namespace Demo1
{
    public class SeedDbInitializer : CreateDatabaseIfNotExists<DemoDbContext>
    {
        protected override void Seed(DemoDbContext context)
        {
            var phongBan1 = new Department { Name = "Phòng Nhân sự" };
            var phongBan2 = new Department { Name = "Phòng Kỹ thuật" };
            context.Departments.Add(phongBan1);
            context.Departments.Add(phongBan2);

            var user = new User
            {
                TenTK = "duongthihoaianh",
                MatKhau = "16022005",
                LoaiTK = true
            };
            context.Users.Add(user);

            context.SaveChanges(); 
        }
    }
}
