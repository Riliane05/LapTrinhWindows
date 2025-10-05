using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vothuyhongphuc_4901103068_CSWinWeek2
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Text = "PicView";
        }
        private void Hinh1_Click(object sender, EventArgs e)
        {
            ChildForm child = new ChildForm();
            child.MdiParent = this;
            child.SetImage(@"Images\hinh1.jpg");
            child.Text = "Hình 1";
            child.Show();
        }
        private void Hinh2_Click(object sender, EventArgs e)
        {
            ChildForm child = new ChildForm();
            child.MdiParent = this;
            child.SetImage(@"Images\hinh2.jpg");
            child.Text = "Hình 2";
            child.Show();
        }

        private void MoFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Chọn ảnh từ máy";
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string selectedImagePath = ofd.FileName;

                ChildForm child = new ChildForm();
                child.MdiParent = this;
                child.SetImage(selectedImagePath);  
                child.Text = System.IO.Path.GetFileName(selectedImagePath);
                child.Show();
            }
        }

        private void XepChong_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void XepNgang_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void XepDoc_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }
    }
}
