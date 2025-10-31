using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Demo1
{
    public partial class FormUser : Form
    {
        List<string> listAccountType = new List<string>() { "Quản Lý", "Nhân Viên" };
        int index = -1;
        List<User> userList = new List<User>();

        public FormUser()
        {
            InitializeComponent();
        }

        void LoadListUser()
        {
            using (var context = new DemoDbContext())
            {
                userList = context.Users.ToList();
                dtgvUser.DataSource = null;
                dtgvUser.DataSource = userList;
                dtgvUser.Columns["ID"].Visible = false;
                dtgvUser.Refresh();
            }
        }

        private void FormUser_Load(object sender, EventArgs e)
        {
            cboStaff.DataSource = listAccountType;
            txbPassWord.PasswordChar = '*';
            LoadListUser();
        }

        private void btnAdd_Click_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string passWord = txbPassWord.Text;
            bool accountType = cboStaff.Text == "Quản Lý";

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(passWord))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên Tài Khoản và Mật Khẩu!", "Cảnh báo");
                return;
            }

            using (var context = new DemoDbContext())
            {
                bool isExist = context.Users.Any(u => u.TenTK.ToLower() == userName.ToLower());

                if (isExist)
                {
                    MessageBox.Show("Tên Tài Khoản đã tồn tại!", "Cảnh Báo");
                    return;
                }

                var user = new User
                {
                    TenTK = userName,
                    MatKhau = passWord,
                    LoaiTK = accountType
                };

                context.Users.Add(user);
                context.SaveChanges();
            }

            MessageBox.Show("Thêm tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadListUser();
        }

        private void dtgvUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;

            if (index < 0 || index >= userList.Count)
                return;

            var user = userList[index];

            txbUserName.Text = user.TenTK;
            cboStaff.Text = user.LoaiTK ? "Quản Lý" : "Nhân Viên";

            if (user.LoaiTK)
            {
                txbPassWord.Text = user.MatKhau;
                txbPassWord.PasswordChar = '\0';
            }
            else
            {
                txbPassWord.Text = "********";
                txbPassWord.PasswordChar = '*'; 
            }
        }


        private void dtgvUser_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dtgvUser.Columns[e.ColumnIndex].Name == "MatKhau" && e.Value != null)
            {
                var loaiTKValue = dtgvUser.Rows[e.RowIndex].Cells["LoaiTK"].Value;

                if (loaiTKValue != null && !(bool)loaiTKValue)  
                {
                    e.Value = "********";
                    e.FormattingApplied = true;
                }
            }
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dtgvUser.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một tài khoản để sửa.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dtgvUser.CurrentRow.Cells["ID"].Value);

            string userName = txbUserName.Text.Trim();
            string passWord = txbPassWord.Text.Trim();
            bool accountType = cboStaff.Text == "Quản Lý";

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(passWord))
            {
                MessageBox.Show("Tên tài khoản và mật khẩu không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn sửa thông tin tài khoản này không?",
                "Xác nhận sửa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            using (var context = new DemoDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.ID == id);
                if (user == null)
                {
                    MessageBox.Show("Tài khoản không tồn tại trong danh sách.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string newPassword = user.MatKhau;
                if (passWord != "********")
                {
                    newPassword = passWord;
                }

                if (user.TenTK == userName && user.MatKhau == newPassword && user.LoaiTK == accountType)
                {
                    MessageBox.Show("Không có thông tin nào được thay đổi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                user.TenTK = userName;
                user.MatKhau = newPassword;
                user.LoaiTK = accountType;

                context.SaveChanges();
            }

            MessageBox.Show("Cập nhật tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadListUser(); 
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dtgvUser.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một tài khoản để xóa.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dtgvUser.CurrentRow.Cells["ID"].Value);

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            using (var context = new DemoDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.ID == id);
                if (user == null)
                {
                    MessageBox.Show("Tài khoản không tồn tại trong danh sách.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                context.Users.Remove(user);
                context.SaveChanges();
            }

            MessageBox.Show("Xóa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadListUser(); 
        }


        private void btnOut_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void dtgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDVMD_Click(object sender, EventArgs e)
        {
            txbPassWord.Text = "12345678";
            txbPassWord.PasswordChar = '\0'; 
        }
    }
}
