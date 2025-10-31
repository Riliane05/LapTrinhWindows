using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.IO;
using System.Windows.Forms;

namespace Demo1
{
    public partial class FormAddNewEmployee : Form
    {

        private string selectedImagePath = null; 

        public FormAddNewEmployee()
        {
            InitializeComponent();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private bool checkInput()
        {
            if (string.IsNullOrWhiteSpace(txbHoTen.Text))
            {
                MessageBox.Show("Họ tên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbHoTen.Focus();
                return false;

            }

            if (string.IsNullOrWhiteSpace(txbDiaChi.Text))
            {
                MessageBox.Show("Địa chỉ không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbDiaChi.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txbMaNV.Text))
            {
                MessageBox.Show("Mã nhân viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbMaNV.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txbChucVu.Text))
            {
                MessageBox.Show("Chức vụ không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbChucVu.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txbLuong.Text))
            {
                MessageBox.Show("Lương không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbLuong.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txbPhuCap.Text))
            {
                MessageBox.Show("Phụ cấp không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbPhuCap.Focus();
                return false;
            }

            return true; 
        }

        private void FromAddNewEmployee_Load(object sender, EventArgs e)
        {
            cboSex.DataSource = Const.listSex;
            cboTrangthai.DataSource = Const.listTrangthai;

            using (var context = new DemoDbContext())
            {
                var listDepartments = context.Departments.ToList();
                cboPhongban.DataSource = listDepartments;
                cboPhongban.DisplayMember = "Name";
                cboPhongban.ValueMember = "Id";
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!checkInput()) return;

            string name = txbHoTen.Text.Trim();
            DateTime birthday = dtpNgaySinh.Value;
            string sex = cboSex.Text;
            string phone = txbSDT.Text.Trim();
            string email = txbEmail.Text.Trim();
            string address = txbDiaChi.Text.Trim();
            string vanHoa = txbVanHoa.Text.Trim();
            string chuyenMon = txbChuyenMon.Text.Trim();
            string maNhanVien = txbMaNV.Text.Trim();
            string chucVu = txbChucVu.Text.Trim();
            string trangThai = cboTrangthai.Text;

            double luong = 0;
            double phuCap = 0;
            byte[] imageBytes = null;

            if (!double.TryParse(txbLuong.Text, out luong))
            {
                MessageBox.Show("Giá trị lương không hợp lệ! Vui lòng nhập số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            if (!double.TryParse(txbPhuCap.Text, out phuCap))
            {
                MessageBox.Show("Phụ cấp không hợp lệ!");
                return;
            }

            if (cboPhongban.SelectedValue == null || !int.TryParse(cboPhongban.SelectedValue.ToString(), out int selectedPhongBanId))
            {
                MessageBox.Show("Vui lòng chọn phòng ban!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var context = new DemoDbContext())
            {
                bool exists = context.Employees.Any(emp => emp.MaNhanVien == maNhanVien);
                if (exists)
                {
                    MessageBox.Show("Mã nhân viên đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!string.IsNullOrEmpty(selectedImagePath) && File.Exists(selectedImagePath))
                {
                    imageBytes = File.ReadAllBytes(selectedImagePath);
                }

                Employee newEmployee = new Employee
                {
                    HoTen = name,
                    NgaySinh = birthday,
                    GioiTinh = sex,
                    SDT = phone,
                    Email = email,
                    DiaChi = address,
                    VanHoa = vanHoa,
                    ChuyenMon = chuyenMon,
                    MaNhanVien = maNhanVien,
                    PhongBanID = selectedPhongBanId,
                    ChucVu = chucVu,
                    TrangThai = trangThai,
                    Luong = luong,
                    PhuCap = phuCap,
                    Avatar = imageBytes
                };
   
                context.Employees.Add(newEmployee);
                context.SaveChanges();
            }

            MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }


        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn thoát?",
                "Xác nhận thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void labelhinhdd_Click(object sender, EventArgs e)
        {
            // nothing
        }

        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn ảnh đại diện";
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    selectedImagePath = ofd.FileName;
                    pictureBoxAvatar.Image = Image.FromFile(selectedImagePath);
                    pictureBoxAvatar.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

    }
}
