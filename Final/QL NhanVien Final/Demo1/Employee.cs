using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo1
{
    public class Employee
    {
        [Key]
        public int Id { get; set; } // khóa chính

        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }

        public string DiaChi { get; set; }
        public string Email { get; set; }
        public string SDT { get; set; }

        public string VanHoa { get; set; }
        public string ChuyenMon { get; set; }

        public string MaNhanVien { get; set; }
        public int? PhongBanID { get; set; }  

        [ForeignKey("PhongBanID")]
        public virtual Department Department { get; set; }
        public string ChucVu { get; set; }
        public string TrangThai { get; set; }

        public double Luong { get; set; }
        public double PhuCap { get; set; }

        public byte[] Avatar { get; set; } // avatar

        //cham cong
        public DateTime? CheckInTime { get; set; } // ko xài
        public DateTime? CheckOutTime { get; set; }

        public Employee(string name, DateTime birthday, string sex, string address, string email, string phone,
                        string vanhoa, string chuyenmon, string manhanvien, int phongban,
                        string chucvu, string trangthai, double luong, double phucap, byte[] avatar = null)
        {
            this.HoTen = name;
            this.NgaySinh = birthday;
            this.GioiTinh = sex;
            this.DiaChi = address;
            this.Email = email;
            this.SDT = phone;
            this.VanHoa = vanhoa;
            this.ChuyenMon = chuyenmon;
            this.MaNhanVien = manhanvien;
            this.PhongBanID = phongban;
            this.ChucVu = chucvu;
            this.TrangThai = trangthai;
            this.Luong = luong;
            this.PhuCap = phucap;
            this.Avatar = avatar;
        }

        public Employee() { }
    }
}
