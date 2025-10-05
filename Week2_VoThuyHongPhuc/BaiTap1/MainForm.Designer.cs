namespace vothuyhongphuc_4901103068_CSWinWeek2
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.hìnhẢnhToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Hinh1 = new System.Windows.Forms.ToolStripMenuItem();
            this.Hinh2 = new System.Windows.Forms.ToolStripMenuItem();
            this.MoFile = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.XepChong = new System.Windows.Forms.ToolStripMenuItem();
            this.XepNgang = new System.Windows.Forms.ToolStripMenuItem();
            this.XepDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hìnhẢnhToolStripMenuItem,
            this.windowToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1474, 50);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // hìnhẢnhToolStripMenuItem
            // 
            this.hìnhẢnhToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Hinh1,
            this.Hinh2,
            this.MoFile});
            this.hìnhẢnhToolStripMenuItem.Name = "hìnhẢnhToolStripMenuItem";
            this.hìnhẢnhToolStripMenuItem.Size = new System.Drawing.Size(150, 42);
            this.hìnhẢnhToolStripMenuItem.Text = "Hình ảnh";
            // 
            // Hinh1
            // 
            this.Hinh1.Name = "Hinh1";
            this.Hinh1.Size = new System.Drawing.Size(306, 46);
            this.Hinh1.Text = "Hình 1";
            this.Hinh1.Click += new System.EventHandler(this.Hinh1_Click);
            // 
            // Hinh2
            // 
            this.Hinh2.Name = "Hinh2";
            this.Hinh2.Size = new System.Drawing.Size(306, 46);
            this.Hinh2.Text = "Hình 2";
            this.Hinh2.Click += new System.EventHandler(this.Hinh2_Click);
            // 
            // MoFile
            // 
            this.MoFile.Name = "MoFile";
            this.MoFile.Size = new System.Drawing.Size(306, 46);
            this.MoFile.Text = "Mở từ máy...";
            this.MoFile.Click += new System.EventHandler(this.MoFile_Click);
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.XepChong,
            this.XepNgang,
            this.XepDoc});
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(138, 42);
            this.windowToolStripMenuItem.Text = "Window";
            // 
            // XepChong
            // 
            this.XepChong.Name = "XepChong";
            this.XepChong.Size = new System.Drawing.Size(287, 46);
            this.XepChong.Text = "Xếp chồng";
            this.XepChong.Click += new System.EventHandler(this.XepChong_Click);
            // 
            // XepNgang
            // 
            this.XepNgang.Name = "XepNgang";
            this.XepNgang.Size = new System.Drawing.Size(287, 46);
            this.XepNgang.Text = "Xếp ngang";
            this.XepNgang.Click += new System.EventHandler(this.XepNgang_Click);
            // 
            // XepDoc
            // 
            this.XepDoc.Name = "XepDoc";
            this.XepDoc.Size = new System.Drawing.Size(287, 46);
            this.XepDoc.Text = "Xếp dọc";
            this.XepDoc.Click += new System.EventHandler(this.XepDoc_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1474, 929);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem hìnhẢnhToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Hinh1;
        private System.Windows.Forms.ToolStripMenuItem Hinh2;
        private System.Windows.Forms.ToolStripMenuItem MoFile;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem XepChong;
        private System.Windows.Forms.ToolStripMenuItem XepNgang;
        private System.Windows.Forms.ToolStripMenuItem XepDoc;
    }
}