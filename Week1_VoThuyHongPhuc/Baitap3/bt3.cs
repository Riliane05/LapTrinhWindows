using System;

namespace BaiTapDaThuc
{
    public class DaThuc
    {
        private int bac;            
        private double[] heSo;     

        public DaThuc(int n)
        {
            bac = n;
            heSo = new double[n + 1];
            for (int i = 0; i <= n; i++)
                heSo[i] = 0;
        }

        public int Bac
        {
            get { return bac; }
        }

        public double this[int k]
        {
            get
            {
                if (k < 0 || k > bac) throw new IndexOutOfRangeException("Chi so khong hop le.");
                return heSo[k];
            }
            set
            {
                if (k < 0 || k > bac) throw new IndexOutOfRangeException("Chi so khong hop le.");
                heSo[k] = value;
            }
        }

        public static DaThuc operator +(DaThuc a, DaThuc b)
        {
            int maxBac = Math.Max(a.bac, b.bac);
            DaThuc result = new DaThuc(maxBac);

            for (int i = 0; i <= maxBac; i++)
            {
                double hsA = (i <= a.bac) ? a[i] : 0;
                double hsB = (i <= b.bac) ? b[i] : 0;
                result[i] = hsA + hsB;
            }

            return result;
        }

        public static DaThuc operator -(DaThuc a, DaThuc b)
        {
            int maxBac = Math.Max(a.bac, b.bac);
            DaThuc result = new DaThuc(maxBac);

            for (int i = 0; i <= maxBac; i++)
            {
                double hsA = (i <= a.bac) ? a[i] : 0;
                double hsB = (i <= b.bac) ? b[i] : 0;
                result[i] = hsA - hsB;
            }

            return result;
        }

        public double TinhGiaTri(double x)
        {
            double ketQua = 0;
            for (int i = 0; i <= bac; i++)
            {
                ketQua += heSo[i] * Math.Pow(x, i);
            }
            return ketQua;
        }

        public override string ToString()
        {
            string s = "";
            bool laSoHangDau = true;

            for (int i = bac; i >= 0; i--)
            {
                double hs = heSo[i];
                if (hs == 0) continue;

                string dau;
                if (laSoHangDau)
                {
                    dau = (hs > 0) ? "" : "-";
                }
                else
                {
                    dau = (hs > 0) ? " + " : " - ";
                }

                double absHs = Math.Abs(hs);
                string hsStr = (absHs == 1 && i != 0) ? "" : absHs.ToString();

                if (i == 0)
                    s += dau + hsStr;
                else if (i == 1)
                    s += dau + hsStr + "x";
                else
                    s += dau + hsStr + $"x^{i}";

                laSoHangDau = false;
            }

            return string.IsNullOrEmpty(s) ? "0" : s;
        }
    }

    class bt3
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            DaThuc P = new DaThuc(2);
            P[0] = 1;
            P[1] = -3;
            P[2] = 2;

            DaThuc Q = new DaThuc(3);
            Q[0] = -2;
            Q[1] = 4;
            Q[3] = 1;

            Console.WriteLine("Da thuc P(x): " + P);
            Console.WriteLine("Da thuc Q(x): " + Q);

            DaThuc Tong = P + Q;
            DaThuc Hieu = P - Q;

            Console.WriteLine("P(x) + Q(x) = " + Tong);
            Console.WriteLine("P(x) - Q(x) = " + Hieu);

            double x = 2;
            Console.WriteLine($"Gia tri P({x}) = {P.TinhGiaTri(x)}");
            Console.WriteLine($"Gia tri Q({x}) = {Q.TinhGiaTri(x)}");

            Console.ReadLine();
        }
    }
}
