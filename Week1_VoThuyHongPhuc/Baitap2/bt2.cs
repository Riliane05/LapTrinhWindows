using System;
using System.Text.RegularExpressions;

namespace QuanLyNhanVien
{
    // Lớp cơ sở: Nguoi
    public class Nguoi
    {
        private string hoTen;
        private DateTime ngaySinh;
        private string diaChi;

        public Nguoi() { }

        public Nguoi(string hoTen, DateTime ngaySinh, string diaChi)
        {
            this.hoTen = hoTen;
            this.ngaySinh = ngaySinh;
            this.diaChi = diaChi;
        }

        public string HoTen
        {
            get { return hoTen; }
            set { hoTen = value; }
        }

        public DateTime NgaySinh
        {
            get { return ngaySinh; }
            set { ngaySinh = value; }
        }

        public string DiaChi
        {
            get { return diaChi; }
            set { diaChi = value; }
        }

        public int Tuoi
        {
            get
            {
                int tuoi = DateTime.Now.Year - ngaySinh.Year;
                if (DateTime.Now < ngaySinh.AddYears(tuoi)) tuoi--;
                return tuoi;
            }
        }

        public virtual void XemThongTin()
        {
            Console.WriteLine($"Ho ten: {HoTen}");
            Console.WriteLine($"Ngay sinh: {NgaySinh.ToShortDateString()}");
            Console.WriteLine($"Dia chi: {DiaChi}");
            Console.WriteLine($"Tuoi: {Tuoi}");
        }
    }

    // Lớp kế thừa: SinhVien
    public class SinhVien : Nguoi
    {
        private string maSV;
        private string maLop;
        private string email;
        private string dienThoai;

        public SinhVien() { }

        public SinhVien(string hoTen, DateTime ngaySinh, string diaChi,
                        string maSV, string maLop, string email, string dienThoai)
            : base(hoTen, ngaySinh, diaChi)
        {
            MaSV = maSV;
            MaLop = maLop;
            Email = email;
            DienThoai = dienThoai;
        }

        public string MaSV
        {
            get { return maSV; }
            set { maSV = value; }
        }

        public string MaLop
        {
            get { return maLop; }
            set { maLop = value; }
        }

        public string Email
        {
            get { return email; }
            set
            {
                if (value.Contains("@")) email = value;
                else email = "Email khong hop le";
            }
        }

        public string DienThoai
        {
            get { return dienThoai; }
            set
            {
                if (Regex.IsMatch(value, @"^\d{10}$")) dienThoai = value;
                else dienThoai = "SDT khong hop le";
            }
        }

        public override void XemThongTin()
        {
            base.XemThongTin();
            Console.WriteLine($"Ma SV: {MaSV}");
            Console.WriteLine($"Ma lop: {MaLop}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Dien thoai: {DienThoai}");
        }
    }

    // Lớp kế thừa: NhanVien
    public class NhanVien : Nguoi
    {
        private string maNV;
        private string email;
        private string dienThoai;
        private DateTime ngayLamViec;
        private string maCongTy;

        public NhanVien() { }

        public NhanVien(string hoTen, DateTime ngaySinh, string diaChi,
                        string maNV, string email, string dienThoai,
                        DateTime ngayLamViec, string maCongTy)
            : base(hoTen, ngaySinh, diaChi)
        {
            MaNhanVien = maNV;
            Email = email;
            DienThoai = dienThoai;
            NgayLamViec = ngayLamViec;
            MaCongTy = maCongTy;
        }

        public string MaNhanVien
        {
            get { return maNV; }
            set { maNV = value; }
        }

        public string Email
        {
            get { return email; }
            set
            {
                if (value.Contains("@")) email = value;
                else email = "Email khong hop le";
            }
        }

        public string DienThoai
        {
            get { return dienThoai; }
            set
            {
                if (Regex.IsMatch(value, @"^\d{10}$")) dienThoai = value;
                else dienThoai = "SDT khong hop le";
            }
        }

        public DateTime NgayLamViec
        {
            get { return ngayLamViec; }
            set { ngayLamViec = value; }
        }

        public string MaCongTy
        {
            get { return maCongTy; }
            set { maCongTy = value; }
        }

        public override void XemThongTin()
        {
            base.XemThongTin();
            Console.WriteLine($"Ma nhan vien: {MaNhanVien}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Dien thoai: {DienThoai}");
            Console.WriteLine($"Ngay lam viec: {NgayLamViec.ToShortDateString()}");
            Console.WriteLine($"Ma cong ty: {MaCongTy}");
        }
    }

    // Hàm Main để chạy chương trình
    class bt2
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("==> Sinh vien:");
            SinhVien sv = new SinhVien(
                "Le Van B", new DateTime(2003, 3, 15), "TP.HCM",
                "SV001", "CTK44", "le.b@example.com", "0912345678"
            );
            sv.XemThongTin();

            Console.WriteLine("\n==> Nhan vien:");
            NhanVien nv = new NhanVien(
                "Tran Thi C", new DateTime(1995, 10, 5), "Da Nang",
                "NV001", "tran.c@example.com", "0987654321",
                new DateTime(2020, 1, 15), "CTY123"
            );
            nv.XemThongTin();

            Console.ReadLine();
        }
    }
}
