using System.Drawing;
using System.Windows.Forms;

namespace AUCWO
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.QADstt = new System.Windows.Forms.Label();
            this.SAPstt = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.LblNG = new System.Windows.Forms.Label();
            this.LblOK = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Prod_LineCBB = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.Btn_Upload = new System.Windows.Forms.Button();
            this.LinkTbx = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Viewdata = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Viewdata)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.LblNG);
            this.panel1.Controls.Add(this.LblOK);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.Prod_LineCBB);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.Btn_Upload);
            this.panel1.Controls.Add(this.LinkTbx);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1126, 178);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.QADstt);
            this.groupBox1.Controls.Add(this.SAPstt);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(942, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(172, 128);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thời gian cập nhật dữ liệu:";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(64, 98);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(65, 24);
            this.button1.TabIndex = 11;
            this.button1.Text = "Cập nhật";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.LoadStatus);
            // 
            // QADstt
            // 
            this.QADstt.AutoSize = true;
            this.QADstt.Location = new System.Drawing.Point(52, 75);
            this.QADstt.Name = "QADstt";
            this.QADstt.Size = new System.Drawing.Size(16, 13);
            this.QADstt.TabIndex = 5;
            this.QADstt.Text = "...";
            // 
            // SAPstt
            // 
            this.SAPstt.AutoSize = true;
            this.SAPstt.Location = new System.Drawing.Point(49, 50);
            this.SAPstt.Name = "SAPstt";
            this.SAPstt.Size = new System.Drawing.Size(22, 13);
            this.SAPstt.TabIndex = 4;
            this.SAPstt.Text = ".....";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(49, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Luôn mới nhất";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 75);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "QAD:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "SAP:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "MES:";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Green;
            this.button3.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.button3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button3.Location = new System.Drawing.Point(787, 49);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(130, 74);
            this.button3.TabIndex = 5;
            this.button3.Text = "Export";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.Btn_Export);
            // 
            // LblNG
            // 
            this.LblNG.AutoSize = true;
            this.LblNG.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNG.ForeColor = System.Drawing.Color.Red;
            this.LblNG.Location = new System.Drawing.Point(641, 143);
            this.LblNG.Name = "LblNG";
            this.LblNG.Size = new System.Drawing.Size(38, 24);
            this.LblNG.TabIndex = 9;
            this.LblNG.Text = "0/0";
            // 
            // LblOK
            // 
            this.LblOK.AutoSize = true;
            this.LblOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblOK.ForeColor = System.Drawing.Color.Green;
            this.LblOK.Location = new System.Drawing.Point(492, 143);
            this.LblOK.Name = "LblOK";
            this.LblOK.Size = new System.Drawing.Size(38, 24);
            this.LblOK.TabIndex = 8;
            this.LblOK.Text = "0/0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(580, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 24);
            this.label4.TabIndex = 7;
            this.label4.Text = "NG:";
            // 
            // Prod_LineCBB
            // 
            this.Prod_LineCBB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Prod_LineCBB.FormattingEnabled = true;
            this.Prod_LineCBB.Items.AddRange(new object[] {
            "IK,GW,RFC,RFS",
            "MD",
            "EVS"});
            this.Prod_LineCBB.Location = new System.Drawing.Point(141, 96);
            this.Prod_LineCBB.Name = "Prod_LineCBB";
            this.Prod_LineCBB.Size = new System.Drawing.Size(104, 21);
            this.Prod_LineCBB.TabIndex = 6;
            this.Prod_LineCBB.SelectedIndexChanged += new System.EventHandler(this.Prod_LineCBB_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Green;
            this.label3.Location = new System.Drawing.Point(437, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 24);
            this.label3.TabIndex = 6;
            this.label3.Text = "OK:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 21);
            this.label2.TabIndex = 5;
            this.label2.Text = "Chọn Bộ phận:";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Gold;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.button2.Location = new System.Drawing.Point(616, 50);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(151, 74);
            this.button2.TabIndex = 4;
            this.button2.Text = "Check WO";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.Check_WO);
            // 
            // Btn_Upload
            // 
            this.Btn_Upload.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.Btn_Upload.Location = new System.Drawing.Point(497, 49);
            this.Btn_Upload.Name = "Btn_Upload";
            this.Btn_Upload.Size = new System.Drawing.Size(86, 33);
            this.Btn_Upload.TabIndex = 3;
            this.Btn_Upload.Text = "Chọn";
            this.Btn_Upload.UseVisualStyleBackColor = true;
            this.Btn_Upload.Click += new System.EventHandler(this.Btn_Upload_Click);
            // 
            // LinkTbx
            // 
            this.LinkTbx.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LinkTbx.Location = new System.Drawing.Point(117, 52);
            this.LinkTbx.Name = "LinkTbx";
            this.LinkTbx.Size = new System.Drawing.Size(361, 29);
            this.LinkTbx.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "File Check:";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.panel3.Controls.Add(this.label7);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1126, 41);
            this.panel3.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(437, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(283, 24);
            this.label7.TabIndex = 10;
            this.label7.Text = "Auto Check Work Order 1.0.0";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.Viewdata);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 178);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1126, 479);
            this.panel2.TabIndex = 1;
            // 
            // Viewdata
            // 
            this.Viewdata.AllowUserToAddRows = false;
            this.Viewdata.AllowUserToDeleteRows = false;
            this.Viewdata.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Viewdata.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.Viewdata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Viewdata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Viewdata.Location = new System.Drawing.Point(0, 0);
            this.Viewdata.Name = "Viewdata";
            this.Viewdata.ReadOnly = true;
            this.Viewdata.Size = new System.Drawing.Size(1126, 479);
            this.Viewdata.TabIndex = 0;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1126, 657);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "Auto Check  WO";
            this.Load += new System.EventHandler(this.Main_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Viewdata)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Button button2;
        private Button Btn_Upload;
        private TextBox LinkTbx;
        private Label label1;
        private Panel panel2;
        private Label label2;
        private ComboBox Prod_LineCBB;
        private Button button3;
        private Label label4;
        private Label label3;
        private DataGridView Viewdata;
        private Label LblNG;
        private Label LblOK;
        private Panel panel3;
        private Label label7;
        private GroupBox groupBox1;
        private Label QADstt;
        private Label SAPstt;
        private Label label9;
        private Label label8;
        private Label label6;
        private Label label5;
        private Button button1;
    }
}

