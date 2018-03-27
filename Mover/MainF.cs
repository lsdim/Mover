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
            if (e.ColumnIndex==2)
            {
                GV2_Btn_Click(e.RowIndex);
            }
        }

        private void GV2_Btn_Click(int RowIndex)
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
                    if (Gv2GetUnic(fbd.SelectedPath, RowIndex))
                        GV2[1, RowIndex].Value = fbd.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private bool Gv2GetUnic(string text, int rowindex)
        {
            bool rez = true;

            foreach (DataGridViewRow row in GV2.Rows)
            {
                if ((String)row.Cells[1].Value == text && row.Index != rowindex)
                {
                    MessageBox.Show(String.Format("Такий шлях вже вибрано в рядку № {0}",row.Cells[0].Value), "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return rez;
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            switch (((Button)sender).Name)
            {
                case "bAdd1":
                    RowAdd(GV1);
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
            Gv2GetUnic((String)GV2[e.ColumnIndex,e.RowIndex].Value, e.RowIndex);
        }

        
    }
}
