using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Week3_VoThuyHongPhuc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void LoadData()
{
    using (var context = new NhanVienDbContext())
    {
        var list = context.NhanViens.ToList();
        dgvNhanVien.DataSource = list;
    }

    // Tuỳ chọn: Ẩn cột Id nếu bạn không muốn người dùng thấy
    dgvNhanVien.Columns["Id"].Visible = false;
}

        private void Form1_Load(object sender, EventArgs e)
        {
            using (var context = new NhanVienDbContext())
            {
                if (!context.NhanViens.Any()) // Nếu bảng chưa có dữ liệu
                {
                    context.NhanViens.AddRange(new List<NhanVien>
            {
                new NhanVien { Ten = "Nguyễn Văn A", Tuoi = 25, Email = "a@gmail.com", PhongBan = "Kế toán" },
                new NhanVien { Ten = "Trần Thị B", Tuoi = 30, Email = "b@gmail.com", PhongBan = "Nhân sự" }
            });
                    context.SaveChanges();
                }
            }
            LoadData();
            dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNhanVien.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];
                txtTen.Text = row.Cells["Ten"].Value.ToString();
                txtTuoi.Text = row.Cells["Tuoi"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtPhongBan.Text = row.Cells["PhongBan"].Value.ToString();
            }
        }
        private void ClearInput()
        {
            txtTen.Text = "";
            txtTuoi.Text = "";
            txtEmail.Text = "";
            txtPhongBan.Text = "";
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtTen.Text) ||
                string.IsNullOrWhiteSpace(txtTuoi.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPhongBan.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return false;
            }

            if (!int.TryParse(txtTuoi.Text, out _))
            {
                MessageBox.Show("Tuổi phải là số nguyên.");
                return false;
            }

            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            using (var context = new NhanVienDbContext())
            {
                var nv = new NhanVien
                {
                    Ten = txtTen.Text,
                    Tuoi = int.Parse(txtTuoi.Text),
                    Email = txtEmail.Text,
                    PhongBan = txtPhongBan.Text
                };
                context.NhanViens.Add(nv);
                context.SaveChanges();
            }

            LoadData();
            ClearInput();
            MessageBox.Show("Đã thêm nhân viên.");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một dòng để sửa.");
                return;
            }

            if (!ValidateInput()) return;

            int id = (int)dgvNhanVien.CurrentRow.Cells["Id"].Value;

            using (var context = new NhanVienDbContext())
            {
                var nv = context.NhanViens.Find(id);
                if (nv != null)
                {
                    nv.Ten = txtTen.Text;
                    nv.Tuoi = int.Parse(txtTuoi.Text);
                    nv.Email = txtEmail.Text;
                    nv.PhongBan = txtPhongBan.Text;

                    context.SaveChanges();
                }
            }

            LoadData();
            ClearInput();
            MessageBox.Show("Đã cập nhật thông tin.");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xoá.");
                return;
            }

            int id = (int)dgvNhanVien.CurrentRow.Cells["Id"].Value;

            var result = MessageBox.Show("Bạn có chắc chắn muốn xoá?", "Xác nhận", MessageBoxButtons.YesNo);
            if (result == DialogResult.No) return;

            using (var context = new NhanVienDbContext())
            {
                var nv = context.NhanViens.Find(id);
                if (nv != null)
                {
                    context.NhanViens.Remove(nv);
                    context.SaveChanges();
                }
            }

            LoadData();
            ClearInput();
            MessageBox.Show("Đã xoá nhân viên.");
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearInput();
            LoadData();
        }

    }

}
