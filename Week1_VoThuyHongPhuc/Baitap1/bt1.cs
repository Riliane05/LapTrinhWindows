using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HinhHocApp
{
    public class HinhHoc
    {
        public double DienTich { get; set; }
        public double ChuVi { get; set; }

        public virtual void tinhDienTichChuVi() { }

        public override string ToString()
        {
            return $"S = {Math.Round(DienTich, 2)}; P = {Math.Round(ChuVi, 2)}.";
        }
    }

    public class HinhChuNhat : HinhHoc
    {
        private double mChieuDai;
        private double mChieuRong;

        public double ChieuDai
        {
            get { return mChieuDai; }
            set
            {
                if (value > 0)
                    mChieuDai = value;
                else
                    throw new Exception("Chieu dai phai lon hon 0");
            }
        }

        public double ChieuRong
        {
            get { return mChieuRong; }
            set
            {
                if (value > 0)
                    mChieuRong = value;
                else
                    throw new Exception("Chieu rong phai lon hon 0");
            }
        }

        public HinhChuNhat(double dai = 1, double rong = 0.5)
        {
            ChieuDai = dai;
            ChieuRong = rong;
        }

        public override void tinhDienTichChuVi()
        {
            DienTich = ChieuDai * ChieuRong;
            ChuVi = 2 * (ChieuDai + ChieuRong);
        }

        public override string ToString()
        {
            return $"L = {ChieuDai}, W = {ChieuRong}; {base.ToString()}";
        }
    }

    public class HinhTron : HinhHoc
    {
        private double mBanKinh;

        public double BanKinh
        {
            get { return mBanKinh; }
            set
            {
                if (value > 0)
                    mBanKinh = value;
                else
                    throw new Exception("Ban kinh phai lon hon 0");
            }
        }

        public HinhTron(double r = 1)
        {
            BanKinh = r;
        }

        public override void tinhDienTichChuVi()
        {
            DienTich = Math.PI * BanKinh * BanKinh;
            ChuVi = 2 * Math.PI * BanKinh;
        }

        public override string ToString()
        {
            return $"R = {BanKinh}; {base.ToString()}";
        }
    }

    class bt1
    {
        static void Main(string[] args)
        {
            string inputPath = "HinhHoc.txt";
            string outputPath = "output.txt";

            List<HinhHoc> danhSach = new List<HinhHoc>();

            try
            {
                if (!File.Exists(inputPath))
                {
                    Console.WriteLine("Khong tim thay file HinhHoc.txt.");
                    return;
                }

                string[] lines = File.ReadAllLines(inputPath);
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    string[] parts = line.Split('\t');

                    int loai = int.Parse(parts[0]);

                    if (loai == 1 && parts.Length == 3)
                    {
                        double dai = double.Parse(parts[1]);
                        double rong = double.Parse(parts[2]);
                        HinhChuNhat hcn = new HinhChuNhat(dai, rong);
                        hcn.tinhDienTichChuVi();
                        danhSach.Add(hcn);
                    }
                    else if (loai == 2 && parts.Length == 2)
                    {
                        double r = double.Parse(parts[1]);
                        HinhTron ht = new HinhTron(r);
                        ht.tinhDienTichChuVi();
                        danhSach.Add(ht);
                    }
                }

                int soHinh = danhSach.Count;
                double tongDienTich = danhSach.Sum(h => h.DienTich);
                double tongChuVi = danhSach.Sum(h => h.ChuVi);
                var hcnMax = danhSach.OfType<HinhChuNhat>().OrderByDescending(h => h.DienTich).FirstOrDefault();
                var htMin = danhSach.OfType<HinhTron>().OrderBy(h => h.ChuVi).FirstOrDefault();

                // Tạo danh sách dòng kết quả
                List<string> ketQua = new List<string>
                {
                    $"So hinh: {soHinh}",
                    $"Tong dien tich: {tongDienTich:F2}",
                    $"Tong chu vi: {tongChuVi:F2}",
                    "Hinh chu nhat co dien tich lon nhat: " + (hcnMax?.ToString() ?? "Khong co"),
                    "Hinh tron co chu vi nho nhat: " + (htMin?.ToString() ?? "Khong co")
                };

                // Ghi ra file
                File.WriteAllLines(outputPath, ketQua, System.Text.Encoding.UTF8);

                // In ra màn hình
                foreach (string dong in ketQua)
                {
                    Console.WriteLine(dong);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loi: " + ex.Message);
            }
        }
    }
}
