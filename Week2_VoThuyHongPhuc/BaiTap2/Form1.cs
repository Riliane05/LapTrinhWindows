using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTap2
{
    public partial class CalculatorForm : Form
    {
        int soThuNhat = 0;
        string toanTuDangChon = "";
        bool dangNhapSoMoi = false;

        public CalculatorForm()
        {
            InitializeComponent();
        }

        private void So_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (dangNhapSoMoi || txtDisplay.Text == "0")
                txtDisplay.Text = btn.Text;
            else
                txtDisplay.Text += btn.Text;
            dangNhapSoMoi = false;
        }

        private void ToanTu_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender; 
            soThuNhat = int.Parse(txtDisplay.Text);
            toanTuDangChon = btn.Text; 
            dangNhapSoMoi = true;
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            int soThuHai = int.Parse(txtDisplay.Text);
            int ketQua = 0;
            switch (toanTuDangChon)
            {
                case "+": ketQua = soThuNhat + soThuHai; break;
                case "-": ketQua = soThuNhat - soThuHai; break;
                case "*": ketQua = soThuNhat * soThuHai; break;
                case "/":
                    if (soThuHai == 0)
                    {
                        MessageBox.Show("Không thể chia cho 0");
                        return;
                    }
                    ketQua = soThuNhat / soThuHai;
                    break;
            }
            txtDisplay.Text = ketQua.ToString();
            dangNhapSoMoi = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
            soThuNhat = 0;
            toanTuDangChon = "";
            dangNhapSoMoi = false;
        }
    }
}
