namespace Mover
{
    partial class MainF
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainF));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.nI1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.cMS1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SettingTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.LogTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.CloseTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.chBbaloon = new System.Windows.Forms.CheckBox();
            this.bRem1 = new System.Windows.Forms.Button();
            this.bAdd1 = new System.Windows.Forms.Button();
            this.Sec = new System.Windows.Forms.NumericUpDown();
            this.p1 = new System.Windows.Forms.Panel();
            this.GV1 = new System.Windows.Forms.DataGridView();
            this.DirList = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Place = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mask = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bRemConf = new System.Windows.Forms.Button();
            this.bAddConf = new System.Windows.Forms.Button();
            this.chBAutoRun = new System.Windows.Forms.CheckBox();
            this.cBconf = new System.Windows.Forms.ComboBox();
            this.chBconf = new System.Windows.Forms.CheckBox();
            this.bCancel = new System.Windows.Forms.Button();
            this.bOK = new System.Windows.Forms.Button();
            this.bRem2 = new System.Windows.Forms.Button();
            this.bAdd2 = new System.Windows.Forms.Button();
            this.p2 = new System.Windows.Forms.Panel();
            this.GV2 = new System.Windows.Forms.DataGridView();
            this.PathScan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Browse = new System.Windows.Forms.DataGridViewButtonColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.cMS1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Sec)).BeginInit();
            this.p1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GV1)).BeginInit();
            this.p2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GV2)).BeginInit();
            this.SuspendLayout();
            // 
            // nI1
            // 
            this.nI1.ContextMenuStrip = this.cMS1;
            this.nI1.Icon = ((System.Drawing.Icon)(resources.GetObject("nI1.Icon")));
            this.nI1.Text = "notifyIcon1";
            this.nI1.Visible = true;
            this.nI1.DoubleClick += new System.EventHandler(this.nI1_DoubleClick);
            // 
            // cMS1
            // 
            this.cMS1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SettingTSMI,
            this.toolStripMenuItem1,
            this.LogTSMI,
            this.toolStripMenuItem2,
            this.CloseTSMI});
            this.cMS1.Name = "cMS1";
            this.cMS1.ShowImageMargin = false;
            this.cMS1.Size = new System.Drawing.Size(133, 82);
            // 
            // SettingTSMI
            // 
            this.SettingTSMI.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.SettingTSMI.Name = "SettingTSMI";
            this.SettingTSMI.Size = new System.Drawing.Size(132, 22);
            this.SettingTSMI.Text = "Налаштування";
            this.SettingTSMI.Click += new System.EventHandler(this.SettingTSMI_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(129, 6);
            // 
            // LogTSMI
            // 
            this.LogTSMI.Name = "LogTSMI";
            this.LogTSMI.Size = new System.Drawing.Size(132, 22);
            this.LogTSMI.Text = "Журнал подій";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(129, 6);
            // 
            // CloseTSMI
            // 
            this.CloseTSMI.Name = "CloseTSMI";
            this.CloseTSMI.Size = new System.Drawing.Size(132, 22);
            this.CloseTSMI.Text = "Вихід";
            this.CloseTSMI.Click += new System.EventHandler(this.CloseTSMenu_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.chBbaloon);
            this.splitContainer1.Panel1.Controls.Add(this.bRem1);
            this.splitContainer1.Panel1.Controls.Add(this.bAdd1);
            this.splitContainer1.Panel1.Controls.Add(this.Sec);
            this.splitContainer1.Panel1.Controls.Add(this.p1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.bRemConf);
            this.splitContainer1.Panel2.Controls.Add(this.bAddConf);
            this.splitContainer1.Panel2.Controls.Add(this.chBAutoRun);
            this.splitContainer1.Panel2.Controls.Add(this.cBconf);
            this.splitContainer1.Panel2.Controls.Add(this.chBconf);
            this.splitContainer1.Panel2.Controls.Add(this.bCancel);
            this.splitContainer1.Panel2.Controls.Add(this.bOK);
            this.splitContainer1.Panel2.Controls.Add(this.bRem2);
            this.splitContainer1.Panel2.Controls.Add(this.bAdd2);
            this.splitContainer1.Panel2.Controls.Add(this.p2);
            this.splitContainer1.Size = new System.Drawing.Size(500, 391);
            this.splitContainer1.SplitterDistance = 210;
            this.splitContainer1.TabIndex = 0;
            // 
            // chBbaloon
            // 
            this.chBbaloon.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.chBbaloon.AutoSize = true;
            this.chBbaloon.Checked = true;
            this.chBbaloon.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chBbaloon.Location = new System.Drawing.Point(259, 180);
            this.chBbaloon.Name = "chBbaloon";
            this.chBbaloon.Size = new System.Drawing.Size(159, 17);
            this.chBbaloon.TabIndex = 4;
            this.chBbaloon.Text = "Показувати повідомлення";
            this.chBbaloon.UseVisualStyleBackColor = true;
            // 
            // bRem1
            // 
            this.bRem1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bRem1.Location = new System.Drawing.Point(53, 176);
            this.bRem1.Name = "bRem1";
            this.bRem1.Size = new System.Drawing.Size(45, 23);
            this.bRem1.TabIndex = 3;
            this.bRem1.Text = "-";
            this.bRem1.UseVisualStyleBackColor = true;
            this.bRem1.Click += new System.EventHandler(this.BRem_Click);
            // 
            // bAdd1
            // 
            this.bAdd1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bAdd1.Location = new System.Drawing.Point(3, 176);
            this.bAdd1.Name = "bAdd1";
            this.bAdd1.Size = new System.Drawing.Size(44, 23);
            this.bAdd1.TabIndex = 2;
            this.bAdd1.Text = "+";
            this.bAdd1.UseVisualStyleBackColor = true;
            this.bAdd1.Click += new System.EventHandler(this.BAdd_Click);
            // 
            // Sec
            // 
            this.Sec.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Sec.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.Sec.Location = new System.Drawing.Point(150, 179);
            this.Sec.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.Sec.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Sec.Name = "Sec";
            this.Sec.Size = new System.Drawing.Size(45, 20);
            this.Sec.TabIndex = 1;
            this.Sec.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Sec.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // p1
            // 
            this.p1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.p1.Controls.Add(this.GV1);
            this.p1.Location = new System.Drawing.Point(3, 3);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(494, 170);
            this.p1.TabIndex = 0;
            // 
            // GV1
            // 
            this.GV1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GV1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GV1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DirList,
            this.Place,
            this.Mask,
            this.Stan});
            this.GV1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GV1.Location = new System.Drawing.Point(0, 0);
            this.GV1.MultiSelect = false;
            this.GV1.Name = "GV1";
            this.GV1.RowHeadersWidth = 50;
            this.GV1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GV1.Size = new System.Drawing.Size(494, 170);
            this.GV1.TabIndex = 0;
            this.GV1.DoubleClick += new System.EventHandler(this.GV1_DoubleClick);
            // 
            // DirList
            // 
            this.DirList.HeaderText = "###";
            this.DirList.Name = "DirList";
            this.DirList.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Place
            // 
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Place.DefaultCellStyle = dataGridViewCellStyle2;
            this.Place.FillWeight = 200F;
            this.Place.HeaderText = "Місце призначення";
            this.Place.Name = "Place";
            this.Place.Width = 129;
            // 
            // Mask
            // 
            this.Mask.HeaderText = "Маска";
            this.Mask.Name = "Mask";
            // 
            // Stan
            // 
            this.Stan.HeaderText = "Стан";
            this.Stan.Name = "Stan";
            this.Stan.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // bRemConf
            // 
            this.bRemConf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bRemConf.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bRemConf.Location = new System.Drawing.Point(74, 155);
            this.bRemConf.Name = "bRemConf";
            this.bRemConf.Size = new System.Drawing.Size(24, 18);
            this.bRemConf.TabIndex = 9;
            this.bRemConf.Text = "-";
            this.bRemConf.UseVisualStyleBackColor = true;
            this.bRemConf.Visible = false;
            this.bRemConf.Click += new System.EventHandler(this.bRemConf_Click);
            // 
            // bAddConf
            // 
            this.bAddConf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bAddConf.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bAddConf.Location = new System.Drawing.Point(222, 155);
            this.bAddConf.Name = "bAddConf";
            this.bAddConf.Size = new System.Drawing.Size(24, 18);
            this.bAddConf.TabIndex = 8;
            this.bAddConf.Text = "+";
            this.bAddConf.UseVisualStyleBackColor = true;
            this.bAddConf.Visible = false;
            this.bAddConf.Click += new System.EventHandler(this.bAddConf_Click);
            // 
            // chBAutoRun
            // 
            this.chBAutoRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chBAutoRun.AutoSize = true;
            this.chBAutoRun.Location = new System.Drawing.Point(259, 132);
            this.chBAutoRun.Name = "chBAutoRun";
            this.chBAutoRun.Size = new System.Drawing.Size(85, 17);
            this.chBAutoRun.TabIndex = 7;
            this.chBAutoRun.Text = "Автозапуск";
            this.chBAutoRun.UseVisualStyleBackColor = true;
            // 
            // cBconf
            // 
            this.cBconf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cBconf.FormattingEnabled = true;
            this.cBconf.Location = new System.Drawing.Point(100, 151);
            this.cBconf.Name = "cBconf";
            this.cBconf.Size = new System.Drawing.Size(121, 21);
            this.cBconf.TabIndex = 6;
            this.cBconf.Visible = false;
            this.cBconf.SelectedIndexChanged += new System.EventHandler(this.cBconf_SelectedIndexChanged);
            // 
            // chBconf
            // 
            this.chBconf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chBconf.AutoSize = true;
            this.chBconf.Location = new System.Drawing.Point(100, 134);
            this.chBconf.Name = "chBconf";
            this.chBconf.Size = new System.Drawing.Size(117, 17);
            this.chBconf.TabIndex = 5;
            this.chBconf.Text = "Декілька конфігів";
            this.chBconf.UseVisualStyleBackColor = true;
            this.chBconf.CheckedChanged += new System.EventHandler(this.chBconf_CheckedChanged);
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(422, 142);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 4;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // bOK
            // 
            this.bOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOK.Location = new System.Drawing.Point(341, 142);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(75, 23);
            this.bOK.TabIndex = 3;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bRem2
            // 
            this.bRem2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bRem2.Location = new System.Drawing.Point(53, 132);
            this.bRem2.Name = "bRem2";
            this.bRem2.Size = new System.Drawing.Size(45, 23);
            this.bRem2.TabIndex = 2;
            this.bRem2.Text = "-";
            this.bRem2.UseVisualStyleBackColor = true;
            this.bRem2.Click += new System.EventHandler(this.BRem_Click);
            // 
            // bAdd2
            // 
            this.bAdd2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bAdd2.Location = new System.Drawing.Point(3, 132);
            this.bAdd2.Name = "bAdd2";
            this.bAdd2.Size = new System.Drawing.Size(44, 23);
            this.bAdd2.TabIndex = 1;
            this.bAdd2.Text = "+";
            this.bAdd2.UseVisualStyleBackColor = true;
            this.bAdd2.Click += new System.EventHandler(this.BAdd_Click);
            // 
            // p2
            // 
            this.p2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.p2.Controls.Add(this.GV2);
            this.p2.Location = new System.Drawing.Point(3, 3);
            this.p2.Name = "p2";
            this.p2.Size = new System.Drawing.Size(494, 125);
            this.p2.TabIndex = 0;
            // 
            // GV2
            // 
            this.GV2.AllowUserToAddRows = false;
            this.GV2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GV2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PathScan,
            this.Browse});
            this.GV2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GV2.Location = new System.Drawing.Point(0, 0);
            this.GV2.MultiSelect = false;
            this.GV2.Name = "GV2";
            this.GV2.RowHeadersWidth = 50;
            this.GV2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GV2.Size = new System.Drawing.Size(494, 125);
            this.GV2.TabIndex = 0;
            this.GV2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GV2_CellClick);
            this.GV2.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.GV2_CellEndEdit);
            // 
            // PathScan
            // 
            this.PathScan.HeaderText = "Перевіряти папки";
            this.PathScan.Name = "PathScan";
            this.PathScan.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PathScan.Width = 350;
            // 
            // Browse
            // 
            this.Browse.HeaderText = "Огляд";
            this.Browse.Name = "Browse";
            this.Browse.Text = "...";
            this.Browse.ToolTipText = "Обрати папку";
            this.Browse.Width = 50;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(413, 179);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 391);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainF";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mover";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainF_FormClosing);
            this.Load += new System.EventHandler(this.MainF_Load);
            this.Resize += new System.EventHandler(this.MainF_Resize);
            this.cMS1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Sec)).EndInit();
            this.p1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GV1)).EndInit();
            this.p2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GV2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel p1;
        private System.Windows.Forms.DataGridView GV1;
        private System.Windows.Forms.Panel p2;
        private System.Windows.Forms.DataGridView GV2;
        private System.Windows.Forms.NumericUpDown Sec;
        private System.Windows.Forms.Button bAdd2;
        private System.Windows.Forms.Button bAdd1;
        private System.Windows.Forms.Button bRem1;
        private System.Windows.Forms.Button bRem2;
        private System.Windows.Forms.ContextMenuStrip cMS1;
        private System.Windows.Forms.ToolStripMenuItem SettingTSMI;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem LogTSMI;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem CloseTSMI;
        public System.Windows.Forms.NotifyIcon nI1;
        private System.Windows.Forms.DataGridViewTextBoxColumn PathScan;
        private System.Windows.Forms.DataGridViewButtonColumn Browse;
        private System.Windows.Forms.DataGridViewTextBoxColumn DirList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Place;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mask;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stan;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bOK;
        public System.Windows.Forms.CheckBox chBbaloon;
        private System.Windows.Forms.CheckBox chBAutoRun;
        private System.Windows.Forms.Button bRemConf;
        private System.Windows.Forms.Button bAddConf;
        private System.Windows.Forms.ComboBox cBconf;
        private System.Windows.Forms.CheckBox chBconf;
        private System.Windows.Forms.Button button1;
    }
}

