using System.Data.Entity;

namespace Demo1
{
    public class DemoDbContext : DbContext
    {        public DemoDbContext() : base("name=DemoDBConnection") { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TimekeepingRecord> TimekeepingRecords { get; set; }

    }
}
