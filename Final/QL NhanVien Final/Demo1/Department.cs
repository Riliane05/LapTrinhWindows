using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo1
{
    public class Department
    {
        [Key]
        public int Id { get; set; }  // Khóa chính

        [Required(ErrorMessage = "Tên phòng ban không được để trống")]
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

        public Department()
        {
            Employees = new HashSet<Employee>();
        }

        public Department(string name) : this()
        {
            this.Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
