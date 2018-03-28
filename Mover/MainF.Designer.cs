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
            this.nI1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.bRem1 = new System.Windows.Forms.Button();
            this.bAdd1 = new System.Windows.Forms.Button();
            this.Sec = new System.Windows.Forms.NumericUpDown();
            this.p1 = new System.Windows.Forms.Panel();
            this.GV1 = new System.Windows.Forms.DataGridView();
            this.NN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DirList = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Place = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mask = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stan = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.bRem2 = new System.Windows.Forms.Button();
            this.bAdd2 = new System.Windows.Forms.Button();
            this.p2 = new System.Windows.Forms.Panel();
            this.GV2 = new System.Windows.Forms.DataGridView();
            this.Ndir = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Browse = new System.Windows.Forms.DataGridViewButtonColumn();
            this.cMS1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.налаштуванняToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.журналПодійToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.вихідToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Sec)).BeginInit();
            this.p1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GV1)).BeginInit();
            this.p2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GV2)).BeginInit();
            this.cMS1.SuspendLayout();
            this.SuspendLayout();
            // 
            // nI1
            // 
            this.nI1.ContextMenuStrip = this.cMS1;
            this.nI1.Icon = ((System.Drawing.Icon)(resources.GetObject("nI1.Icon")));
            this.nI1.Text = "notifyIcon1";
            this.nI1.Visible = true;
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
            this.splitContainer1.Panel1.Controls.Add(this.bRem1);
            this.splitContainer1.Panel1.Controls.Add(this.bAdd1);
            this.splitContainer1.Panel1.Controls.Add(this.Sec);
            this.splitContainer1.Panel1.Controls.Add(this.p1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.bRem2);
            this.splitContainer1.Panel2.Controls.Add(this.bAdd2);
            this.splitContainer1.Panel2.Controls.Add(this.p2);
            this.splitContainer1.Size = new System.Drawing.Size(500, 410);
            this.splitContainer1.SplitterDistance = 221;
            this.splitContainer1.TabIndex = 0;
            // 
            // bRem1
            // 
            this.bRem1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bRem1.Location = new System.Drawing.Point(53, 187);
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
            this.bAdd1.Location = new System.Drawing.Point(3, 187);
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
            this.Sec.Location = new System.Drawing.Point(150, 190);
            this.Sec.Name = "Sec";
            this.Sec.Size = new System.Drawing.Size(45, 20);
            this.Sec.TabIndex = 1;
            // 
            // p1
            // 
            this.p1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.p1.Controls.Add(this.GV1);
            this.p1.Location = new System.Drawing.Point(3, 3);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(494, 181);
            this.p1.TabIndex = 0;
            // 
            // GV1
            // 
            this.GV1.AllowUserToAddRows = false;
            this.GV1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GV1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NN,
            this.DirList,
            this.Place,
            this.Mask,
            this.Stan});
            this.GV1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GV1.Location = new System.Drawing.Point(0, 0);
            this.GV1.Name = "GV1";
            this.GV1.Size = new System.Drawing.Size(494, 181);
            this.GV1.TabIndex = 0;
            // 
            // NN
            // 
            this.NN.Frozen = true;
            this.NN.HeaderText = "№";
            this.NN.Name = "NN";
            this.NN.ReadOnly = true;
            this.NN.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.NN.Width = 30;
            // 
            // DirList
            // 
            this.DirList.HeaderText = "###";
            this.DirList.Name = "DirList";
            // 
            // Place
            // 
            this.Place.HeaderText = "Місце призначення";
            this.Place.Name = "Place";
            // 
            // Mask
            // 
            this.Mask.HeaderText = "Маска";
            this.Mask.Name = "Mask";
            // 
            // Stan
            // 
            this.Stan.HeaderText = "Стан";
            this.Stan.Items.AddRange(new object[] {
            "0 - нічого не робити",
            "1 - копіювати",
            "2 - перемістити",
            "3 - видалити",
            "4 - запустити файл",
            "5 - виконати командний рядок",
            "6 - показати повідомлення",
            "7 - перейменувати",
            "8 - видалити всі крім вказаних"});
            this.Stan.Name = "Stan";
            // 
            // bRem2
            // 
            this.bRem2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bRem2.Location = new System.Drawing.Point(53, 142);
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
            this.bAdd2.Location = new System.Drawing.Point(3, 142);
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
            this.p2.Size = new System.Drawing.Size(494, 133);
            this.p2.TabIndex = 0;
            // 
            // GV2
            // 
            this.GV2.AllowUserToAddRows = false;
            this.GV2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GV2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Ndir,
            this.Path,
            this.Browse});
            this.GV2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GV2.Location = new System.Drawing.Point(0, 0);
            this.GV2.Name = "GV2";
            this.GV2.Size = new System.Drawing.Size(494, 133);
            this.GV2.TabIndex = 0;
            this.GV2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GV2_CellClick);
            this.GV2.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.GV2_CellEndEdit);
            // 
            // Ndir
            // 
            this.Ndir.Frozen = true;
            this.Ndir.HeaderText = "№";
            this.Ndir.Name = "Ndir";
            this.Ndir.ReadOnly = true;
            this.Ndir.Width = 30;
            // 
            // Path
            // 
            this.Path.HeaderText = "Перевіряти папки";
            this.Path.Name = "Path";
            this.Path.Width = 300;
            // 
            // Browse
            // 
            this.Browse.HeaderText = "Огляд";
            this.Browse.Name = "Browse";
            this.Browse.Text = "...";
            this.Browse.ToolTipText = "Обрати папку";
            // 
            // cMS1
            // 
            this.cMS1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.налаштуванняToolStripMenuItem,
            this.toolStripMenuItem1,
            this.журналПодійToolStripMenuItem,
            this.toolStripMenuItem2,
            this.вихідToolStripMenuItem});
            this.cMS1.Name = "cMS1";
            this.cMS1.ShowImageMargin = false;
            this.cMS1.Size = new System.Drawing.Size(133, 104);
            // 
            // налаштуванняToolStripMenuItem
            // 
            this.налаштуванняToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.налаштуванняToolStripMenuItem.Name = "налаштуванняToolStripMenuItem";
            this.налаштуванняToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.налаштуванняToolStripMenuItem.Text = "Налаштування";
            // 
            // журналПодійToolStripMenuItem
            // 
            this.журналПодійToolStripMenuItem.Name = "журналПодійToolStripMenuItem";
            this.журналПодійToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.журналПодійToolStripMenuItem.Text = "Журнал подій";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(129, 6);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(129, 6);
            // 
            // вихідToolStripMenuItem
            // 
            this.вихідToolStripMenuItem.Name = "вихідToolStripMenuItem";
            this.вихідToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.вихідToolStripMenuItem.Text = "Вихід";
            this.вихідToolStripMenuItem.Click += new System.EventHandler(this.вихідToolStripMenuItem_Click);
            // 
            // MainF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 410);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainF";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mover";
            this.Load += new System.EventHandler(this.MainF_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Sec)).EndInit();
            this.p1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GV1)).EndInit();
            this.p2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GV2)).EndInit();
            this.cMS1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon nI1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel p1;
        private System.Windows.Forms.DataGridView GV1;
        private System.Windows.Forms.Panel p2;
        private System.Windows.Forms.DataGridView GV2;
        private System.Windows.Forms.NumericUpDown Sec;
        private System.Windows.Forms.DataGridViewTextBoxColumn NN;
        private System.Windows.Forms.DataGridViewComboBoxColumn DirList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Place;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mask;
        private System.Windows.Forms.DataGridViewComboBoxColumn Stan;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ndir;
        private System.Windows.Forms.DataGridViewTextBoxColumn Path;
        private System.Windows.Forms.DataGridViewButtonColumn Browse;
        private System.Windows.Forms.Button bAdd2;
        private System.Windows.Forms.Button bAdd1;
        private System.Windows.Forms.Button bRem1;
        private System.Windows.Forms.Button bRem2;
        private System.Windows.Forms.ContextMenuStrip cMS1;
        private System.Windows.Forms.ToolStripMenuItem налаштуванняToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem журналПодійToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem вихідToolStripMenuItem;
    }
}

