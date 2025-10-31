using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Demo1
{
    public partial class FormTimekeeping_Admin_ : Form
    {
        private DemoDbContext db = new DemoDbContext();
        private Employee selectedEmployee;

        public FormTimekeeping_Admin_()
        {
            InitializeComponent();
            LoadEmployeeList();
            dgvEmployee.SelectionChanged += dgvEmployee_SelectionChanged;
            dtpNgayChamCong.ValueChanged += (s, e) => LoadEmployeeList();
        }
        private void ApplyRowColorBasedOnTimekeeping()
        {
            foreach (DataGridViewRow row in dgvEmployee.Rows)
            {
                string checkIn = row.Cells["colCheckIn"].Value?.ToString();
                string checkOut = row.Cells["colCheckOut"].Value?.ToString();

                if (!string.IsNullOrEmpty(checkIn) && !string.IsNullOrEmpty(checkOut))
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else if (!string.IsNullOrEmpty(checkIn))
                {
                    row.DefaultCellStyle.BackColor = Color.LightYellow;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.LightCoral;
                }
            }
        }
        private void LoadEmployeeList()
        {
            DateTime selectedDate = dtpNgayChamCong.Value.Date;

            var data = db.Employees
                .Include(e => e.Department)
                .GroupJoin(
                    db.TimekeepingRecords.Where(r => DbFunctions.TruncateTime(r.Date) == selectedDate),
                    e => e.Id,
                    r => r.EmployeeId,
                    (e, records) => new { e, Record = records.FirstOrDefault() }
                )
                .ToList()
                .Select(x => new
                {
                    x.e.Id,
                    x.e.HoTen,
                    x.e.GioiTinh,
                    NgaySinh = x.e.NgaySinh.ToString("dd/MM/yyyy"),
                    x.e.MaNhanVien,
                    PhongBan = x.e.Department != null ? x.e.Department.Name : "Chưa phân phòng",
                    CheckIn = x.Record?.CheckInTime.HasValue == true ? x.Record.CheckInTime.Value.ToString("HH:mm:ss") : "",
                    CheckOut = x.Record?.CheckOutTime.HasValue == true ? x.Record.CheckOutTime.Value.ToString("HH:mm:ss") : ""
                })
                .ToList();

            dgvEmployee.DataSource = data;
            ApplyRowColorBasedOnTimekeeping();
            dgvEmployee.ClearSelection();

            if (dgvEmployee.Rows.Count > 0)
            {
                dgvEmployee.Rows[0].Selected = true;
                dgvEmployee_SelectionChanged(null, null);
            }
            else
            {
                selectedEmployee = null;
            }
        }



        private void dgvEmployee_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEmployee.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvEmployee.SelectedRows[0].Cells["Id"].Value);
                selectedEmployee = db.Employees.FirstOrDefault(emp => emp.Id == id);
            }
            else
            {
                selectedEmployee = null;
            }
        }

        //private void btnCheckIn_Click(object sender, EventArgs e)
        //{
        //    if (selectedEmployee == null)
        //    {
        //        MessageBox.Show("Vui lòng chọn một nhân viên để chấm công vào.", "Thông báo");
        //        return;
        //    }

        //    DateTime selectedDate = dtpNgayChamCong.Value.Date;

        //    var record = db.TimekeepingRecords.FirstOrDefault(r =>
        //        r.EmployeeId == selectedEmployee.Id &&
        //        DbFunctions.TruncateTime(r.Date) == selectedDate);

        //    if (record == null)
        //    {
        //        record = new TimekeepingRecord
        //        {
        //            EmployeeId = selectedEmployee.Id,
        //            Date = selectedDate,
        //        };
        //        db.TimekeepingRecords.Add(record);
        //    }

        //    if (record.CheckInTime != null)
        //    {
        //        MessageBox.Show("Nhân viên này đã chấm công vào hôm nay!", "Thông báo");
        //        return;
        //    }

        //    record.CheckInTime = DateTime.Now;
        //    db.SaveChanges();

        //    MessageBox.Show("Chấm công vào thành công!", "Thông báo");
        //    LoadEmployeeList();
        //    ApplyRowColorBasedOnTimekeeping();
        //}

        //private void btnCheckOut_Click(object sender, EventArgs e)
        //{
        //    if (selectedEmployee == null)
        //    {
        //        MessageBox.Show("Vui lòng chọn một nhân viên để chấm công ra.", "Thông báo");
        //        return;
        //    }

        //    DateTime selectedDate = dtpNgayChamCong.Value.Date;

        //    var record = db.TimekeepingRecords.FirstOrDefault(r =>
        //        r.EmployeeId == selectedEmployee.Id &&
        //        DbFunctions.TruncateTime(r.Date) == selectedDate);

        //    if (record == null)
        //    {
        //        MessageBox.Show("Chưa chấm công vào, không thể chấm công ra!", "Thông báo");
        //        return;
        //    }

        //    if (record.CheckOutTime != null)
        //    {
        //        MessageBox.Show("Đã chấm công ra hôm nay!", "Thông báo");
        //        return;
        //    }

        //    record.CheckOutTime = DateTime.Now;
        //    db.SaveChanges();

        //    MessageBox.Show("Chấm công ra thành công!", "Thông báo");
        //    LoadEmployeeList();
        //    ApplyRowColorBasedOnTimekeeping();
        //}


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string hoTen = txbHoTen.Text.Trim().ToLower();
            string gioiTinh = cboGioiTinh.SelectedItem?.ToString();
            string maNV = txbMaNV.Text.Trim().ToLower();
            DateTime selectedDate = dtpNgayChamCong.Value.Date;

            var filteredEmployees = db.Employees
                .Include(emp => emp.Department)
                .ToList()
                .Where(emp =>
                    (string.IsNullOrEmpty(hoTen) || emp.HoTen.ToLower().Contains(hoTen)) &&
                    (string.IsNullOrEmpty(gioiTinh) || emp.GioiTinh == gioiTinh) &&
                    (string.IsNullOrEmpty(maNV) || emp.MaNhanVien.ToLower().Contains(maNV))
                )
                .ToList();

            var result = filteredEmployees
                .GroupJoin(
                    db.TimekeepingRecords.Where(r => DbFunctions.TruncateTime(r.Date) == selectedDate),
                    emp => emp.Id,
                    r => r.EmployeeId,
                    (emp, records) => new { emp, Record = records.FirstOrDefault() }
                )
                .Select(x => new
                {
                    x.emp.Id,
                    x.emp.HoTen,
                    x.emp.GioiTinh,
                    NgaySinh = x.emp.NgaySinh.ToString("dd/MM/yyyy"),
                    x.emp.MaNhanVien,
                    PhongBan = x.emp.Department != null ? x.emp.Department.Name : "Chưa phân phòng",
                    CheckIn = x.Record?.CheckInTime.HasValue == true ? x.Record.CheckInTime.Value.ToString("HH:mm:ss") : "",
                    CheckOut = x.Record?.CheckOutTime.HasValue == true ? x.Record.CheckOutTime.Value.ToString("HH:mm:ss") : ""
                })
                .ToList();

            dgvEmployee.DataSource = result;
            ApplyRowColorBasedOnTimekeeping();
            dgvEmployee.ClearSelection();
            selectedEmployee = null;

            if (result.Count == 0)
            {
                MessageBox.Show("Không tìm thấy nhân viên nào phù hợp.", "Thông báo");
            }
        }

        private void dgvEmployee_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ApplyRowColorBasedOnTimekeeping();
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txbMaNV_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}
