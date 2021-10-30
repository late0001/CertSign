
namespace CertSign
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.DragRecvBtn = new System.Windows.Forms.Button();
            this.driverPathTb = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.colHeader_pn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHeader_path = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHeader_arch = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHeader_status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_ms_sign = new System.Windows.Forms.Button();
            this.btnCompression = new System.Windows.Forms.Button();
            this.btnSign = new System.Windows.Forms.Button();
            this.btnExitPySrv = new System.Windows.Forms.Button();
            this.btn_StartCli = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.webSourceControl1 = new Awesomium.Windows.Forms.WebSourceControl();
            this.btn_clearlist = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DragRecvBtn
            // 
            this.DragRecvBtn.AllowDrop = true;
            this.DragRecvBtn.Location = new System.Drawing.Point(6, 6);
            this.DragRecvBtn.Name = "DragRecvBtn";
            this.DragRecvBtn.Size = new System.Drawing.Size(946, 115);
            this.DragRecvBtn.TabIndex = 0;
            this.DragRecvBtn.Text = "Drag folder here";
            this.DragRecvBtn.UseVisualStyleBackColor = true;
            this.DragRecvBtn.Click += new System.EventHandler(this.DragRecvBtn_Click);
            this.DragRecvBtn.DragDrop += new System.Windows.Forms.DragEventHandler(this.DragRecvBtn_DragDrop);
            this.DragRecvBtn.DragEnter += new System.Windows.Forms.DragEventHandler(this.DragRecvBtn_DragEnter);
            // 
            // driverPathTb
            // 
            this.driverPathTb.Location = new System.Drawing.Point(29, 20);
            this.driverPathTb.Name = "driverPathTb";
            this.driverPathTb.Size = new System.Drawing.Size(604, 25);
            this.driverPathTb.TabIndex = 1;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(1, 528);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(966, 19);
            this.progressBar1.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(1, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(970, 372);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.driverPathTb);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(779, 343);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btn_clearlist);
            this.tabPage2.Controls.Add(this.webSourceControl1);
            this.tabPage2.Controls.Add(this.listView1);
            this.tabPage2.Controls.Add(this.btn_ms_sign);
            this.tabPage2.Controls.Add(this.btnCompression);
            this.tabPage2.Controls.Add(this.btnSign);
            this.tabPage2.Controls.Add(this.btnExitPySrv);
            this.tabPage2.Controls.Add(this.btn_StartCli);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(962, 343);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colHeader_pn,
            this.colHeader_path,
            this.colHeader_arch,
            this.colHeader_status});
            this.listView1.Location = new System.Drawing.Point(4, 55);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(958, 285);
            this.listView1.TabIndex = 9;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // colHeader_pn
            // 
            this.colHeader_pn.Text = "Product Name";
            this.colHeader_pn.Width = 150;
            // 
            // colHeader_path
            // 
            this.colHeader_path.Text = "Path";
            this.colHeader_path.Width = 563;
            // 
            // colHeader_arch
            // 
            this.colHeader_arch.Text = "Arch";
            this.colHeader_arch.Width = 75;
            // 
            // colHeader_status
            // 
            this.colHeader_status.Text = "Status";
            this.colHeader_status.Width = 94;
            // 
            // btn_ms_sign
            // 
            this.btn_ms_sign.Location = new System.Drawing.Point(492, 15);
            this.btn_ms_sign.Name = "btn_ms_sign";
            this.btn_ms_sign.Size = new System.Drawing.Size(101, 30);
            this.btn_ms_sign.TabIndex = 8;
            this.btn_ms_sign.Text = "MS Sign";
            this.btn_ms_sign.UseVisualStyleBackColor = true;
            this.btn_ms_sign.Click += new System.EventHandler(this.btn_ms_sign_Click);
            // 
            // btnCompression
            // 
            this.btnCompression.Location = new System.Drawing.Point(280, 15);
            this.btnCompression.Name = "btnCompression";
            this.btnCompression.Size = new System.Drawing.Size(88, 29);
            this.btnCompression.TabIndex = 7;
            this.btnCompression.Text = "压缩";
            this.btnCompression.UseVisualStyleBackColor = true;
            this.btnCompression.Click += new System.EventHandler(this.btnCompression_Click);
            // 
            // btnSign
            // 
            this.btnSign.Location = new System.Drawing.Point(379, 13);
            this.btnSign.Name = "btnSign";
            this.btnSign.Size = new System.Drawing.Size(102, 32);
            this.btnSign.TabIndex = 6;
            this.btnSign.Text = "Sign";
            this.btnSign.UseVisualStyleBackColor = true;
            this.btnSign.Click += new System.EventHandler(this.btnSign_Click);
            // 
            // btnExitPySrv
            // 
            this.btnExitPySrv.Location = new System.Drawing.Point(599, 15);
            this.btnExitPySrv.Name = "btnExitPySrv";
            this.btnExitPySrv.Size = new System.Drawing.Size(111, 30);
            this.btnExitPySrv.TabIndex = 5;
            this.btnExitPySrv.Text = "ExitPyServer";
            this.btnExitPySrv.UseVisualStyleBackColor = true;
            this.btnExitPySrv.Click += new System.EventHandler(this.btnExitPySrv_Click);
            // 
            // btn_StartCli
            // 
            this.btn_StartCli.Location = new System.Drawing.Point(159, 13);
            this.btn_StartCli.Name = "btn_StartCli";
            this.btn_StartCli.Size = new System.Drawing.Size(107, 32);
            this.btn_StartCli.TabIndex = 4;
            this.btn_StartCli.Text = "StartClient";
            this.btn_StartCli.UseVisualStyleBackColor = true;
            this.btn_StartCli.Click += new System.EventHandler(this.btn_StartCli_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(18, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 32);
            this.button1.TabIndex = 3;
            this.button1.Text = "StartPyServer";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button2);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(779, 343);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(64, 53);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 38);
            this.button2.TabIndex = 0;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Location = new System.Drawing.Point(5, 374);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(966, 153);
            this.tabControl2.TabIndex = 6;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.DragRecvBtn);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(958, 124);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.richTextBox1);
            this.tabPage5.Location = new System.Drawing.Point(4, 25);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(771, 124);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "tabPage5";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(0, -1);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(757, 121);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.statusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 550);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(971, 26);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(53, 20);
            this.toolStripStatusLabel1.Text = "--:--:--";
            // 
            // statusLabel2
            // 
            this.statusLabel2.Name = "statusLabel2";
            this.statusLabel2.Size = new System.Drawing.Size(21, 20);
            this.statusLabel2.Text = "--";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // webSourceControl1
            // 
            this.webSourceControl1.Location = new System.Drawing.Point(523, 143);
            this.webSourceControl1.Name = "webSourceControl1";
            this.webSourceControl1.Size = new System.Drawing.Size(75, 23);
            this.webSourceControl1.TabIndex = 10;
            // 
            // btn_clearlist
            // 
            this.btn_clearlist.Location = new System.Drawing.Point(766, 15);
            this.btn_clearlist.Name = "btn_clearlist";
            this.btn_clearlist.Size = new System.Drawing.Size(87, 29);
            this.btn_clearlist.TabIndex = 11;
            this.btn_clearlist.Text = "清空";
            this.btn_clearlist.UseVisualStyleBackColor = true;
            this.btn_clearlist.Click += new System.EventHandler(this.btn_clearlist_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 576);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.progressBar1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button DragRecvBtn;
        private System.Windows.Forms.TextBox driverPathTb;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_StartCli;
        private System.Windows.Forms.Button btnExitPySrv;
        private System.Windows.Forms.Button btnSign;
        private System.Windows.Forms.Button btnCompression;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btn_ms_sign;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader colHeader_pn;
        private System.Windows.Forms.ColumnHeader colHeader_path;
        private System.Windows.Forms.ColumnHeader colHeader_arch;
        private System.Windows.Forms.ColumnHeader colHeader_status;
        private System.Windows.Forms.Button btn_clearlist;
        private Awesomium.Windows.Forms.WebSourceControl webSourceControl1;
    }
}

