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
    public partial class ChildForm : Form
    {
        public ChildForm()
        {
            InitializeComponent();
        }

        public void SetImage(string imagePath)
        {
            pictureBox1.Image = Image.FromFile(imagePath);
            try
            {
                Image img = Image.FromFile(imagePath);
                pictureBox1.Image = img;
                pictureBox1.Size = img.Size;
                pictureBox1.Left = (this.ClientSize.Width - pictureBox1.Width) / 2;
                pictureBox1.Top = (this.ClientSize.Height - pictureBox1.Height) / 2;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở ảnh: " + ex.Message);
            }
        }
    }
}
