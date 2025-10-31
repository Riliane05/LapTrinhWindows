using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Demo1
{
    public partial class FormShowInfoEmployee : Form
    {
        public FormShowInfoEmployee()
        {
            InitializeComponent();
        }

        void LoadInfo()
        {
            if (Const.NewEmploy == null)
            {
                MessageBox.Show("Không có dữ liệu để chỉnh sửa", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            using (var db = new DemoDbContext())
            {
                var freshEmp = db.Employees
                                 .Include(emp => emp.Department)
                                 .FirstOrDefault(emp => emp.Id == Const.NewEmploy.Id);

                if (freshEmp == null)
                {
                    MessageBox.Show("Không tìm thấy nhân viên trong cơ sở dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                Const.NewEmploy = freshEmp;

                txbName.Text = freshEmp.HoTen;
                dtpYear.Value = freshEmp.NgaySinh;

                txbSex.Text = freshEmp.GioiTinh;
                txbSDT.Text = freshEmp.SDT;
                txbMail.Text = freshEmp.Email;
                txbAddress.Text = freshEmp.DiaChi;
                txbLearn.Text = freshEmp.VanHoa;
                txbJob.Text = freshEmp.ChuyenMon;

                txbMaNV.Text = freshEmp.MaNhanVien;
                txbDepartment.Text = freshEmp.Department?.Name ?? "Chưa có phòng ban";
                txbChucVu.Text = freshEmp.ChucVu;
                txbContract.Text = freshEmp.TrangThai;
                txbLuong.Text = freshEmp.Luong.ToString();
                txbPhuCap.Text = freshEmp.PhuCap.ToString();

                if (freshEmp.Avatar != null)
                {
                    using (var ms = new System.IO.MemoryStream(freshEmp.Avatar))
                    {
                        pictureBoxViewAvatar.Image = Image.FromStream(ms);
                        pictureBoxViewAvatar.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
                else
                {
                    pictureBoxViewAvatar.Image = null;
                }
            }
        }


        private void FromShowInfoEmployee_Load(object sender, EventArgs e)
        {
            LoadInfo();
            dtpYear.ShowUpDown = true; 
            dtpYear.Enabled = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
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

        private void pictureBoxViewAvatar_Click(object sender, EventArgs e)
        {
            
        }

    }
}
