using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mover
{
    public partial class MainF : Form
    {
        public MainF()
        {
            InitializeComponent();
        }

        private void MainF_Load(object sender, EventArgs e)
        {
            
        }

        private void GV2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2) //is column with buttons
            {
                GV2_Btn_Click(e.RowIndex);
            }
        }

        private void GV2_Btn_Click(int RowIndex) //click button in Grid2
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.ShowNewFolderButton = true;
                fbd.Description = "Оберіть папку для скнування:";
                fbd.SelectedPath = (String)GV2[1, RowIndex].Value;
                DialogResult dr = fbd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    if (Gv2IsUnic(fbd.SelectedPath, RowIndex))
                        GV2[1, RowIndex].Value = fbd.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private bool Gv2IsUnic(string text, int rowindex) //Check is unic record in Grid2
        {
            bool rez = true;

            foreach (DataGridViewRow row in GV2.Rows)
            {
                if ((String)row.Cells[1].Value == text && row.Index != rowindex)
                {
                    MessageBox.Show(String.Format("Такий шлях вже вибрано в рядку № {0}", row.Cells[0].Value), "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return rez;
        }

        

        private void BAdd_Click(object sender, EventArgs e) //buttons "+" Click
        {
         /*   var daysOfWeek =
                new[] { "Monday", "Tuesday", "Wednesday",
                        "Thursday", "Friday", "Saturday",
                        "Sunday" };
*/
            switch (((Button)sender).Name)
            {
                case "bAdd1":
                    RowAdd(GV1);
                    //  GV1.Columns.Add(new ComboBoxColumn(daysOfWeek)); 

                   MessageBox.Show( MoverWork.Mask("\\\\10.202.14.155\\D:\\fpensia\\<yyyy.mm>").clear_mask);
                    break;
                case "bAdd2":
                    RowAdd(GV2);
                    break;
            }
        }

        private void RowAdd(DataGridView GV)
        {
            GV.Rows.Add();
            GV[0, GV.Rows.Count - 1].Value = GV.Rows.Count;
            GV.CurrentCell = GV.Rows[GV.Rows.Count - 1].Cells[0];
        }

        private void GV2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Gv2IsUnic((String)GV2[e.ColumnIndex, e.RowIndex].Value, e.RowIndex);
        }

        private void BRem_Click(object sender, EventArgs e)
        {
            switch (((Button)sender).Name)
            {
                case "bRem1":
                    RowRem(GV1);
                    break;
                case "bRem2":
                    RowRem(GV2);
                    break;
            }
        }

        private void RowRem(DataGridView GV)
        {
            if (GV.Rows.Count <= 0)
                return;

            GV.Rows.Remove(GV.Rows[GV.CurrentCell.RowIndex]);

            if (GV.Rows.Count <= 0)
                return;

            for (int i = GV.CurrentCell.RowIndex; i < GV.Rows.Count; i++)
            {
                GV[0, i].Value = i + 1;
            }
        }

        private void CloseTSMenu_Click(object sender, EventArgs e)
        {            
                Close();
        }

        private void MainF_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                this.Hide();           
        }

        private void SettingTSMI_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }


        private void nI1_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                this.Hide();
                this.WindowState = FormWindowState.Minimized;
            }
            else
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void MainF_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Ви дійсно бажаєте завершити роботу? ", "Mover", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
               e.Cancel = true;
        }

    }
}
