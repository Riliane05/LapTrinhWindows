using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Week3_VoThuyHongPhuc
{
    public class NhanVienDbContext : DbContext
    {
        public NhanVienDbContext() : base("name=NhanVienConnection") { }

        public DbSet<NhanVien> NhanViens { get; set; }
    }
}
