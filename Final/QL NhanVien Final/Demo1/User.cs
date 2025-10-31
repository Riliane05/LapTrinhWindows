using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo1
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; } // Khóa chính tự tăng

        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        [StringLength(50)]

        public string TenTK { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(100)]
        public string MatKhau { get; set; }

        [Required]
        public bool LoaiTK { get; set; } // true: Admin, false: Nhân viên

        public User() { }

        public User(string userName, string passWord, bool accountType)
        {
            this.TenTK = userName;
            this.MatKhau = passWord;
            this.LoaiTK = accountType;
        }
    }
}
