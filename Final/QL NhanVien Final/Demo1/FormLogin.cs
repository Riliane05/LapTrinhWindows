using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo1;
using System.Windows.Forms;

namespace Demo1
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            txbPassword.UseSystemPasswordChar = true;
        }

        bool CheckLogin(string userName, string passWord)
        {
            using (var context = new DemoDbContext())
            {
                var user = context.Users
                            .FirstOrDefault(u => u.TenTK == userName && u.MatKhau == passWord);

                if (user != null)
                {
                    Const.AccountType = user.LoaiTK;
                    return true;
                }

                return false;
            }
        }



        private void btnLogin_Click(object sender, EventArgs e) // dang nhap
        {
            string userName = txbUserName.Text;
            string passWord = txbPassword.Text;

            if (CheckLogin(userName, passWord))
            {
                FormMain f = new FormMain();
                f.Show();
                this.Hide();
                f.Logout += F_Logout;
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbUserName.Focus();
            }
        }

        private void F_Logout(object sender, EventArgs e)
        {
            (sender as FormMain).isExit = false;
            (sender as FormMain).Close();
            this.Show();
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

        private void FromLogin_FromClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txbPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }





        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
