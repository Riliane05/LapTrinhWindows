using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Demo1; 
using System.Data.Entity;


namespace Demo1
{
    public partial class FormMain : Form
    {
        int index = -1;
        public bool isExit = true;
        public event EventHandler Logout;

        private DemoDbContext db = new DemoDbContext();
        private List<Employee> listEmployee = new List<Employee>();

        public FormMain()
        {
            InitializeComponent();
        }

        #region Method

        public void ExportFile(DataTable dataTable, string sheetName, string title)
        {
            Microsoft.Office.Interop.Excel.Application oExcel = new Microsoft.Office.Interop.Excel.Application();

            Microsoft.Office.Interop.Excel.Workbooks oBooks;
            Microsoft.Office.Interop.Excel.Sheets oSheets;
            Microsoft.Office.Interop.Excel.Workbook oBook;
            Microsoft.Office.Interop.Excel.Worksheet oSheet;


            oExcel.Visible = true;
            oExcel.DisplayAlerts = false;
            oExcel.Application.SheetsInNewWorkbook = 1;

            oBooks = oExcel.Workbooks;

            oBook = (Microsoft.Office.Interop.Excel.Workbook)(oExcel.Workbooks.Add(Type.Missing));

            oSheets = oBook.Worksheets;

            oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oSheets.get_Item(1);

            oSheet.Name = sheetName;


            Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "G1");

            head.MergeCells = true;
            head.Value2 = title;
            head.Font.Bold = true;
            head.Font.Name = "Times New Roman";
            head.Font.Size = "20";
            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;



            Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A3", "A3");
            cl1.Value2 = "Mã nhân viên";
            cl1.ColumnWidth = 15;

            Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");
            cl2.Value2 = "Họ tên";
            cl2.ColumnWidth = 25;

            Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");
            cl3.Value2 = "Giới tính";
            cl3.ColumnWidth = 10;

            Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");
            cl4.Value2 = "Ngày sinh";
            cl4.ColumnWidth = 15;

            Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("E3", "E3");
            cl5.Value2 = "Phòng ban";
            cl5.ColumnWidth = 15;

            Microsoft.Office.Interop.Excel.Range cl6 = oSheet.get_Range("F3", "F3");
            cl6.Value2 = "Chức vụ";
            cl6.ColumnWidth = 19;

            Microsoft.Office.Interop.Excel.Range cl7 = oSheet.get_Range("G3", "G3");
            cl7.Value2 = "Trạng thái";
            cl7.ColumnWidth = 20;

            Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "G3");
            rowHead.Font.Bold = true;

            rowHead.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
            rowHead.Interior.ColorIndex = 6;
            rowHead.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;




            object[,] arr = new object[dataTable.Rows.Count, dataTable.Columns.Count];

            for (int row = 0; row < dataTable.Rows.Count; row++)
            {
                DataRow dataRow = dataTable.Rows[row];

                for (int col = 0; col < dataTable.Columns.Count; col++)
                {
                    arr[row, col] = dataRow[col];
                }
            }



            int rowStart = 4;
            int columnStart = 1;
            int rowEnd = rowStart + dataTable.Rows.Count - 2;
            int columnEnd = dataTable.Columns.Count;

            Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, columnStart];

            Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnEnd];

            Microsoft.Office.Interop.Excel.Range range = oSheet.get_Range(c1, c2);

            range.Value2 = arr;

            range.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;

            oSheet.get_Range(c1, c2).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

        }


        void Decentralization()
        {
            if (Const.AccountType == false)
            {
                tsmiUser.Enabled = tsmiEmployee.Enabled = tsmiDepartment.Enabled = tsQLChamCong.Enabled = trợGiúpToolStripMenuItem.Enabled = thốngKêToolStripMenuItem.Enabled = tsmQuanLy.Enabled = false;
            }
        }



        void LoadListEmploy()
        {
            dtgvEmployee.Rows.Clear();

            listEmployee = db.Employees
                .Include(e => e.Department) 
                .ToList(); 

            foreach (var item in listEmployee)
            {
                dtgvEmployee.Rows.Add(
                    item.HoTen,
                    item.GioiTinh,
                    item.NgaySinh.ToShortDateString(),
                    item.MaNhanVien,
                    item.Department?.Name ?? "Chưa phân phòng",
                    item.ChucVu,
                    item.TrangThai
                );
            }
        }

        #endregion

        #region Event

        private void FromMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isExit)
                Application.Exit();
        }

        private void FromMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isExit)
            {
                if (MessageBox.Show("Bạn muốn thoát chương trình", "Cảnh báo", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    e.Cancel = true;
            }
        }

        private void dangxuatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logout(this, new EventArgs());
        }

        private void InitForm()
        {
            btnShow.Enabled = false;
            btnAddNew.Enabled = btnEdit.Enabled = btnDelete.Enabled = false;

            Decentralization();
            LoadListEmploy();

            txbHoTen.Text = "";
            txbMaNV.Text = "";
            cboGioiTinh.SelectedIndex = -1;

            index = -1;
            Const.NewEmploy = null;
        }


        private void FromMain_Load(object sender, EventArgs e)
        {
            InitForm();
        }


        private void tsmiEmployee_Click(object sender, EventArgs e)
        {
            btnShow.Enabled = true;
            btnAddNew.Enabled = btnEdit.Enabled = btnDelete.Enabled = true;
        }

        private void tsmiUser_Click(object sender, EventArgs e)
        {
            FormUser f = new FormUser();
            f.ShowDialog();
        }

        private void tsmiDepartment_Click(object sender, EventArgs e)
        {
            FormDepartment f = new FormDepartment();
            f.ShowDialog();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (index < 0 || index >= listEmployee.Count)
            {
                MessageBox.Show("Hãy chọn 1 bản ghi");
                return;
            }

            int id = listEmployee[index].Id;

            Const.NewEmploy = db.Employees
                .Include(emp => emp.Department)
                .FirstOrDefault(emp => emp.Id == id);


            FormShowInfoEmployee f = new FormShowInfoEmployee();
            f.ShowDialog();
        }

        private void dtgvEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;
            if (index < 0 || index >= listEmployee.Count)
                return;

            Const.NewEmploy = listEmployee[index];
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            Const.NewEmploy = null;
            FormAddNewEmployee f = new FormAddNewEmployee();
            f.FormClosed += F_FormClosed;
            f.ShowDialog();
        }

        private void F_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Const.NewEmploy != null)
            {
                db.Employees.Add(Const.NewEmploy);
                db.SaveChanges();
                LoadListEmploy();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (index < 0 || index >= listEmployee.Count)
            {
                MessageBox.Show("Hãy chọn 1 bản ghi");
                return;
            }

            int id = listEmployee[index].Id;

            string maNV = db.Employees
                .Where(emp => emp.Id == id)
                .Select(emp => emp.MaNhanVien)
                .FirstOrDefault();

            FormEditEmployee f = new FormEditEmployee(maNV);
            f.FormClosed += F_FormClosed1;
            f.ShowDialog();
        }

        private void F_FormClosed1(object sender, FormClosedEventArgs e)
        {
            LoadListEmploy();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (index < 0 || index >= listEmployee.Count)
            {
                MessageBox.Show("Hãy chọn 1 bản ghi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa nhân viên này không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                var emp = listEmployee[index];
                db.Employees.Remove(emp);
                db.SaveChanges();
                LoadListEmploy();
                btnCapNhap.PerformClick();

            }
        }

        private void thốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();

            DataColumn col1 = new DataColumn("MaNhanVien");
            DataColumn col2 = new DataColumn("HoTen");
            DataColumn col3 = new DataColumn("GioiTinh");
            DataColumn col4 = new DataColumn("NgaySinh");
            DataColumn col5 = new DataColumn("PhongBan");
            DataColumn col6 = new DataColumn("ChucVu");
            DataColumn col7 = new DataColumn("TrangThai");

            dataTable.Columns.Add(col1);
            dataTable.Columns.Add(col2);
            dataTable.Columns.Add(col3);
            dataTable.Columns.Add(col4);
            dataTable.Columns.Add(col5);
            dataTable.Columns.Add(col6);
            dataTable.Columns.Add(col7);

            foreach (DataGridViewRow dtgvRow in dtgvEmployee.Rows)
            {
                DataRow dtrow = dataTable.NewRow();

                dtrow[0] = dtgvRow.Cells[3].Value;
                dtrow[1] = dtgvRow.Cells[0].Value;
                dtrow[2] = dtgvRow.Cells[1].Value;
                dtrow[3] = dtgvRow.Cells[2].Value;
                dtrow[4] = dtgvRow.Cells[4].Value;
                dtrow[5] = dtgvRow.Cells[5].Value;
                dtrow[6] = dtgvRow.Cells[6].Value;

                dataTable.Rows.Add(dtrow);
            }

            ExportFile(dataTable, "danh sach", "DANH SÁCH NHÂN VIÊN");
        }

        private void chấmCôngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void chấmCôngNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTimekeeping form = new FormTimekeeping();
            form.ShowDialog();
        }

        private void quảnLýChấmCôngNVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTimekeeping_Admin_ form = new FormTimekeeping_Admin_();
            form.ShowDialog();
        }


        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string hoTen = txbHoTen.Text.Trim().ToLower();
            string gioiTinh = cboGioiTinh.SelectedItem?.ToString(); 
            string maNV = txbMaNV.Text.Trim().ToLower();

            var result = db.Employees
                .Include(emp => emp.Department)
                .ToList()
                .Where(emp =>
                    (string.IsNullOrEmpty(hoTen) || emp.HoTen.ToLower().Contains(hoTen)) &&
                    (string.IsNullOrEmpty(gioiTinh) || emp.GioiTinh == gioiTinh) &&
                    (string.IsNullOrEmpty(maNV) || emp.MaNhanVien.ToLower().Contains(maNV))
                )
                .ToList();

            listEmployee = result;

            dtgvEmployee.Rows.Clear();
            foreach (var item in listEmployee)
            {
                dtgvEmployee.Rows.Add(
                    item.HoTen,
                    item.GioiTinh,
                    item.NgaySinh.ToShortDateString(),
                    item.MaNhanVien,
                    item.Department?.Name ?? "Chưa phân phòng",
                    item.ChucVu,
                    item.TrangThai
                );
            }

            index = -1;
            Const.NewEmploy = null;

            if (listEmployee.Count == 0)
            {
                MessageBox.Show("Không tìm thấy nhân viên nào phù hợp.", "Thông báo");
            }
        }

        private void tsmQuanLy_Click(object sender, EventArgs e)
        {

        }




        #endregion

        private void btnCapNhap_Click(object sender, EventArgs e)
        {
            db = new DemoDbContext();

            txbHoTen.Text = "";
            txbMaNV.Text = "";
            cboGioiTinh.SelectedIndex = -1;

            LoadListEmploy();
            dtgvEmployee.ClearSelection(); 

            MessageBox.Show("Đã làm mới danh sách nhân viên.", "Thông báo");
        }

        private void trợGiúpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string huongDan = "HƯỚNG DẪN SỬ DỤNG CHO QUẢN LÝ:\n\n" +
                              "- Chọn 'Quản lý tài khoản' để chỉnh sửa tài khoản hệ thống.\n\n" +
                              "- Chọn hàng 'Nhân viên' bên dưới danh sách bất kỳ để thao tác : \n" +
                              "+ Nhấn 'Thêm' để thêm nhân viên mới.\n" +
                              "+ Nhấn 'Sửa' để chỉnh sửa thông tin nhân viên đã chọn.\n" +
                              "+ Nhấn 'Xóa' để xoá nhân viên đã chọn.\n\n" +
                              "- Dùng chức năng 'Tìm kiếm' theo Họ tên, Mã NV, Giới tính.\n" +
                              "- Chọn 'Chấm công NV' để ghi lại giờ làm của nhân viên.\n" +
                              "- Chọn 'Quản Lý Chấm công NV' để hiển thị lại giờ làm của nhân viên.\n" +
                              "- Chọn 'Thống kê' để xuất danh sách nhân viên ra Excel.\n\n" +
                              
                              "Mọi thắc mắc vui lòng liên hệ quản trị viên.";

            MessageBox.Show(huongDan, "Trợ giúp", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
