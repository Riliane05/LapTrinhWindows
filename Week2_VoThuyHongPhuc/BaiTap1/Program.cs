using System;
using System.Windows.Forms;
using vothuyhongphuc_4901103068_CSWinWeek2;

namespace BaiTap1
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
