using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;

namespace Demo1
{
    public partial class FormDepartment : Form
    {
        private int selectedDepartmentId = -1;

        public FormDepartment()
        {
            InitializeComponent();
        }

        void LoadListDepartment()
        {
            using (var db = new DemoDbContext())
            {
                var departments = db.Departments.ToList();
                lbDepartment.DataSource = departments;
                lbDepartment.DisplayMember = "Name";
                lbDepartment.ValueMember = "Id";
                txbDepartmentName.Clear();
                selectedDepartmentId = -1;
            }
        }

        private void FormDepartment_Load(object sender, EventArgs e)
        {
            LoadListDepartment();
        }

        private void lbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbDepartment.SelectedItem is Department selectedDept)
            {
                txbDepartmentName.Text = selectedDept.Name;
                selectedDepartmentId = selectedDept.Id;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string departmentName = txbDepartmentName.Text.Trim();

            if (string.IsNullOrEmpty(departmentName))
            {
                MessageBox.Show("Vui lòng nhập tên phòng ban.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var db = new DemoDbContext())
            {
                bool exists = db.Departments.Any(d => d.Name.ToLower() == departmentName.ToLower());
                if (exists)
                {
                    MessageBox.Show("Tên phòng ban đã tồn tại.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var newDept = new Department { Name = departmentName };
                db.Departments.Add(newDept);
                db.SaveChanges();
            }

            LoadListDepartment();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string departmentName = txbDepartmentName.Text.Trim();

            if (string.IsNullOrEmpty(departmentName))
            {
                MessageBox.Show("Vui lòng nhập tên phòng ban.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (selectedDepartmentId == -1)
            {
                MessageBox.Show("Vui lòng chọn phòng ban cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var db = new DemoDbContext())
            {
                var department = db.Departments.Find(selectedDepartmentId);
                if (department == null)
                {
                    MessageBox.Show("Phòng ban không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool exists = db.Departments
                    .Any(d => d.Id != selectedDepartmentId && d.Name.ToLower() == departmentName.ToLower());

                if (exists)
                {
                    MessageBox.Show("Tên phòng ban đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                department.Name = departmentName;
                db.SaveChanges();
            }

            LoadListDepartment();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedDepartmentId == -1)
            {
                MessageBox.Show("Vui lòng chọn phòng ban cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var db = new DemoDbContext())
            {
                var department = db.Departments.Find(selectedDepartmentId);
                if (department == null)
                {
                    MessageBox.Show("Phòng ban không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool hasEmployees = db.Employees.Any(empItem => empItem.PhongBanID == selectedDepartmentId);
                if (hasEmployees)
                {
                    MessageBox.Show("Không thể xóa phòng ban có nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa phòng ban \"{department.Name}\" không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    db.Departments.Remove(department);
                    db.SaveChanges();
                    LoadListDepartment();
                }
            }
        }

        private void btnOut_Click(object sender, EventArgs e)
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
    }
}
