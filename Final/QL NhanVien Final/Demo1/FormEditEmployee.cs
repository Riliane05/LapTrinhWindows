using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;

namespace Demo1
{
    public partial class FormEditEmployee : Form
    {
        private string _maNhanVien;
        private string selectedImagePath = null;

        public FormEditEmployee(string maNhanVien)
        {
            InitializeComponent();
            _maNhanVien = maNhanVien;
            MessageBox.Show("Mã nhân viên sửa là: " + _maNhanVien, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        void LoadInfo()
        {
            using (var db = new DemoDbContext())
            {
                var emp = db.Employees
                            .Include(e => e.Department)
                            .FirstOrDefault(e => e.MaNhanVien == _maNhanVien);

                if (emp == null)
                {
                    MessageBox.Show("Không tìm thấy nhân viên trong cơ sở dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                txbName.Text = emp.HoTen;
                dtpYear.Value = emp.NgaySinh;
                cboSex.Text = emp.GioiTinh;
                txbSDT.Text = emp.SDT;
                txbMail.Text = emp.Email;
                txbAddress.Text = emp.DiaChi;
                txbLearn.Text = emp.VanHoa;
                txbJob.Text = emp.ChuyenMon;

                txbMaNV.Text = emp.MaNhanVien;
                txbMaNV.ReadOnly = true;

                txbChucVu.Text = emp.ChucVu;
                cboContract.Text = emp.TrangThai;
                txbLuong.Text = emp.Luong.ToString();
                txbPhuCap.Text = emp.PhuCap.ToString();

                cboDepartment.SelectedValue = emp.PhongBanID;

                if (emp.Avatar != null)
                {
                    using (var ms = new System.IO.MemoryStream(emp.Avatar))
                    {
                        pictureBoxEditAvatar.Image = Image.FromStream(ms);
                        pictureBoxEditAvatar.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
                else
                {
                    pictureBoxEditAvatar.Image = null;
                }
            }
        }

        private void FormEditEmployee_Load(object sender, EventArgs e)
        {
            cboSex.DataSource = Const.listSex;
            cboContract.DataSource = Const.listTrangthai;

            using (var db = new DemoDbContext())
            {
                var departments = db.Departments.ToList();
                cboDepartment.DataSource = departments;
                cboDepartment.DisplayMember = "Name";
                cboDepartment.ValueMember = "Id";
            }

            LoadInfo();
        }

        private bool checkInput()
        {
            if (string.IsNullOrWhiteSpace(txbName.Text))
            {
                MessageBox.Show("Họ tên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbName.Focus(); return false;
            }

            if (string.IsNullOrWhiteSpace(txbAddress.Text))
            {
                MessageBox.Show("Địa chỉ không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbAddress.Focus(); return false;
            }

            if (string.IsNullOrWhiteSpace(txbMaNV.Text))
            {
                MessageBox.Show("Mã nhân viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbMaNV.Focus(); return false;
            }

            if (string.IsNullOrWhiteSpace(txbChucVu.Text))
            {
                MessageBox.Show("Chức vụ không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbChucVu.Focus(); return false;
            }

            if (string.IsNullOrWhiteSpace(txbLuong.Text))
            {
                MessageBox.Show("Lương không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbLuong.Focus(); return false;
            }

            if (string.IsNullOrWhiteSpace(txbPhuCap.Text))
            {
                MessageBox.Show("Phụ cấp không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbPhuCap.Focus(); return false;
            }

            return true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!checkInput()) return;

            using (var db = new DemoDbContext())
            {
                var emp = db.Employees.FirstOrDefault(empItem => empItem.MaNhanVien == _maNhanVien);
                if (emp == null)
                {
                    MessageBox.Show("Không tìm thấy nhân viên trong danh sách!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                emp.HoTen = txbName.Text;
                emp.NgaySinh = dtpYear.Value;
                emp.GioiTinh = cboSex.Text;
                emp.SDT = txbSDT.Text;
                emp.Email = txbMail.Text;
                emp.DiaChi = txbAddress.Text;
                emp.VanHoa = txbLearn.Text;
                emp.ChuyenMon = txbJob.Text;
                emp.ChucVu = txbChucVu.Text;
                emp.TrangThai = cboContract.Text;

                if (!double.TryParse(txbLuong.Text, out double luong))
                {
                    MessageBox.Show("Lương không hợp lệ!"); return;
                }
                emp.Luong = luong;

                if (!double.TryParse(txbPhuCap.Text, out double phuCap))
                {
                    MessageBox.Show("Phụ cấp không hợp lệ!"); return;
                }
                emp.PhuCap = phuCap;

                if (cboDepartment.SelectedValue == null)
                {
                    MessageBox.Show("Bạn chưa chọn phòng ban!");
                    return;
                }
                emp.PhongBanID = (int)cboDepartment.SelectedValue;

                db.Entry(emp).State = EntityState.Modified;

                if (!string.IsNullOrEmpty(selectedImagePath) && System.IO.File.Exists(selectedImagePath))
                {
                    emp.Avatar = System.IO.File.ReadAllBytes(selectedImagePath);
                }

                db.SaveChanges();
            }

            MessageBox.Show("Cập nhật nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void pictureBoxEditAvatar_Click(object sender, EventArgs e)
        {

        }

        private void btnEditImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn ảnh đại diện mới";
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    selectedImagePath = ofd.FileName;
                    pictureBoxEditAvatar.Image = Image.FromFile(selectedImagePath);
                    pictureBoxEditAvatar.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

    }
}
