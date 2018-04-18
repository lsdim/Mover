using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace Mover
{
    public partial class MainF : Form
    {
        public string vers = new Version(Application.ProductVersion).ToString();
        private static Log addlog = new Log();
        private string urlUPD = "";
        private string confIP;

        public MainF()
        {
            InitializeComponent();
        }

        public NotifyIcon GetNotifyIcon()
        {
            return nI1;
        }



        private void MainF_Load(object sender, EventArgs e)
        {
            addlog.Info("**--**--**Mover v{0}**--**--**", vers);
            addlog.Info("**--**--**Mover v{0}**--**--**", UpdateApl.GetChecksumm(Application.ExecutablePath));
            addlog.Info("***Програму запущенно {0}***", Application.ExecutablePath);


            MoverWeb.MoverServ ms = new MoverWeb.MoverServ();

            string guid = Marshal.GetTypeLibGuidForAssembly(Assembly.GetExecutingAssembly()).ToString();

            Mutex mutexObj = new Mutex(true, guid, out bool existed);

            if (!existed)
            {
                addlog.Info(String.Format("Спроба подвійного запуску", vers));
                Process.GetCurrentProcess().Kill();
            }

            ReadIni();

            //Check Update in host
            // UpdateApl upd = new Mover.UpdateApl(urlUPD);
            //  upd.Download();

            nI1.Text = "Mover server v." + vers;



        }

        private void ReadIni()
        {
            IniFiles ini = new IniFiles(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Properties.ini"));
            int countDir = ini.ReadInteger("MOOOVER", "CountDir", 0);
            int count = ini.ReadInteger("MOOOVER", "Count", 0);

            GV1.RowCount = count;
            GV2.RowCount = countDir;

            for (int i = 0; i < count; i++)
            {
                GV1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                GV1[0, i].Value = ini.ReadString("DIRNOM", "dirNOM." + (i + 1).ToString(), "");
                GV1[1, i].Value = ini.ReadString("HOSTS", "Host." + (i + 1).ToString(), "");
                GV1[2, i].Value = ini.ReadString("HOSTS_MASK", "Host Mask." + (i + 1).ToString(), "");
                GV1[3, i].Value = ini.ReadString("STAN", "Stan." + (i + 1).ToString(), "");
            }

            GV1.AutoResizeColumn(0);
            //GV1.AutoResizeColumn(1);
            GV1.AutoResizeColumn(3);
            GV1.Columns[1].Width = GV1.Columns[1].GetPreferredWidth(DataGridViewAutoSizeColumnMode.DisplayedCells, false);//.FillWeight = 20;

            // GV1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            //GV1.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders);

            for (int i = 0; i < countDir; i++)
            {
                GV2.Rows[i].HeaderCell.Value = (i + 1).ToString();
                GV2[0, i].Value = ini.ReadString("DIR", "dir." + (i + 1).ToString(), "");
                GV2[1, i].Value = "...";
            }
            //GV2.Columns[0].Width = GV2.Columns[0].GetPreferredWidth(DataGridViewAutoSizeColumnMode.DisplayedCells, false);//.FillWeight = 20;
            //  GV2.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;// .AutoResizeColumns();

            Sec.Value = ini.ReadInteger("TIME", "sek", 100);
            confIP = ini.ReadString("MOOOVER", "IP", "0.0.0.0");

            urlUPD = ini.ReadString("UPD", "source", "");
            if (urlUPD == "")
            {
                ini.WriteString("UPD", "source", "http://10.202.14.15/mover/");
                urlUPD = "http://10.202.14.15/mover/";
            }

            chBbaloon.Checked = ini.ReadBool("LOG", "ballon", true);

        }

        private void GV2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1) //is column with buttons
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
                        GV2[0, RowIndex].Value = fbd.SelectedPath;
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
                if ((String)row.Cells[0].Value == text && row.Index != rowindex)
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
                    //MoverWork mw = new MoverWork(@"D:\\11111\\22222\\", GV2[1, GV2.Rows.Count - 1].Value.ToString(), "vedo3.XLs;*.det ", MoverWork.Operation.DeleteExcept);
                    //mw.Run();
                    //MessageBox.Show( MoverWork.Mask("\\\\10.202.14.155\\D:\\fpensia\\<yyyy.mm>").clear_mask);
                    break;
                case "bAdd2":
                    RowAdd(GV2);
                    break;
            }
        }

        private void RowAdd(DataGridView GV)
        {
            try
            {
                GV.Rows.Add();
                GV.Rows[GV.Rows.Count - 1].HeaderCell.Value = GV.Rows.Count.ToString();
                GV.CurrentCell = GV.Rows[GV.Rows.Count - 1].Cells[0];

                if (GV.Name == "GV2")
                    GV.Rows[GV.Rows.Count - 1].Cells[1].Value = "...";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GV2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Gv2IsUnic((String)GV2[e.ColumnIndex, e.RowIndex].Value, e.RowIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            try
            {
                if (GV.Rows.Count <= 0)
                    return;

                int r = GV.CurrentCell.RowIndex + 1;

                GV.Rows.Remove(GV.Rows[GV.CurrentCell.RowIndex]);

                if (GV.Rows.Count <= 0)
                    return;

                for (int i = GV.CurrentCell.RowIndex; i < GV.Rows.Count; i++)
                {
                    GV.Rows[i].HeaderCell.Value = (i + 1).ToString();
                }

                if (GV.Name == "GV2")
                {
                    foreach (DataGridViewRow row in GV1.Rows)
                    {

                        if (int.TryParse(row.Cells[0].Value.ToString(), out int ndir))
                        {
                            if (ndir == r)
                                row.Cells[0].Value = "0";
                            if (ndir > r)
                                row.Cells[0].Value = (ndir - 1).ToString();
                        }
                        else
                        {
                            string[] dirs = row.Cells[0].Value.ToString().Split(',');
                            string tmp = "";
                            for (int i = 0; i < dirs.Count(); i++)
                            {
                                if (int.TryParse(dirs[i], out ndir))
                                {
                                    if (ndir == r)
                                        if (i == 0)
                                            tmp = "0";
                                        else
                                            tmp += ", 0";

                                    if (ndir > r)
                                        if (i == 0)
                                            tmp = (ndir - 1).ToString();
                                        else
                                            tmp += ", " + (ndir - 1).ToString();
                                    if (ndir < r)
                                        if (i == 0)
                                            tmp = (ndir).ToString();
                                        else
                                            tmp += ", " + (ndir).ToString();
                                }
                                else
                                {
                                    if (i == 0)
                                        tmp = "0";
                                    else
                                        tmp += ", 0";
                                }
                            }

                            row.Cells[0].Value = tmp;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CloseTSMenu_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainF_Resize(object sender, EventArgs e)
        {
            //if (this.WindowState == FormWindowState.Minimized)
            //    MinimizMainForm();
            // this.Hide();
            //TODO : Minimize!!!
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
                MinimizMainForm();
                //this.Hide();
                //this.WindowState = FormWindowState.Minimized;
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
            {
                e.Cancel = true;
            }
            else
                addlog.Info("***Завершено роботу***", vers);
        }

        private void GV1_DoubleClick(object sender, EventArgs e)
        {
            // TODO : Form for editing
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            MinimizMainForm();
        }

        private void MinimizMainForm()
        {
            DialogResult dlgRez = MessageBox.Show("Зберегти зміни? ", "Mover", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (dlgRez == DialogResult.Cancel)
                return;
            if (dlgRez == DialogResult.Yes)
            {
                BackgroundWorker wrk = new BackgroundWorker();
                wrk.DoWork += BGW_WriteIni_DoWork;
                wrk.RunWorkerAsync();
            }
            if (dlgRez == DialogResult.No)
                ReadIni();

            this.Hide();
            this.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// Write parametr into ini files
        /// </summary>
        private void WriteIni()
        {
            IniFiles ini = new IniFiles(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Properties.ini"));

            ini.WriteString("MOOOVER", "CountDir", GV2.RowCount.ToString());
            ini.WriteString("MOOOVER", "Count", GV1.RowCount.ToString());

            for (int i = 0; i < GV1.RowCount; i++)
            {
                ini.WriteString("DIRNOM", "dirNOM." + (i + 1).ToString(), GV1[0, i].Value.ToString());
                ini.WriteString("HOSTS", "Host." + (i + 1).ToString(), GV1[1, i].Value.ToString());
                ini.WriteString("HOSTS_MASK", "Host Mask." + (i + 1).ToString(), GV1[2, i].Value.ToString());
                ini.WriteString("STAN", "Stan." + (i + 1).ToString(), GV1[3, i].Value.ToString());
            }

            for (int i = 0; i < GV2.RowCount; i++)
            {
                ini.WriteString("DIR", "dir." + (i + 1).ToString(), GV2[0, i].Value.ToString());
            }

            ini.WriteString("TIME", "sek", Sec.Value.ToString());
            ini.WriteString("LOG", "ballon", Convert.ToInt32(chBbaloon.Checked).ToString());

        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            ReadIni();
            this.Hide();
            this.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// BackGroundWorker DoWork for Write ini file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BGW_WriteIni_DoWork(object sender, DoWorkEventArgs e)
        {
            WriteIni();
        }

        /// <summary>
        /// Get hash for check config in server
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Hash(string s)
        {
            Encoding utf8 = Encoding.GetEncoding("UTF-8");
            // Encoding win1251 = Encoding.GetEncoding("Windows-1251");

            byte[] utf8Bytes = utf8.GetBytes(s);
            //  byte[] win1251Bytes = Encoding.Convert(win1251, utf8, utf8Bytes);

            //    s = UTF8ToWin1251(s);

            const int p = 31;
            long hash = 0, p_pow = 1;
            // int x;
            /*  for (int i = 0; i < s.Length; ++i)
              {                
                  x = s[i];
                  hash = hash + (s[i] - 'a' + 1) * p_pow;
                  p_pow = p_pow * p;
              }
  */
            foreach (byte x in utf8Bytes)
            {
                hash = hash + (x - 'a' + 1) * p_pow;
                p_pow = p_pow * p;
            }

            return hash.ToString();
        }

        private void GetConfFromServer()
        {
            string strToHash = "";

            foreach (DataGridViewRow row in GV1.Rows)
            {
                strToHash += String.Format("{0}{1}{2}{3}", row.Cells[0].Value, row.Cells[1].Value, row.Cells[2].Value, row.Cells[3].Value);
            }
            foreach (DataGridViewRow row in GV2.Rows)
            {
                strToHash += String.Format("{0}", row.Cells[0].Value);
            }
            strToHash += String.Format("{0}{1}", Sec.Value.ToString(), Convert.ToInt32(chBbaloon.Checked));

            MoverWeb.MoverServ MW = new MoverWeb.MoverServ();

            string response = MW.GetCnf(Hash(strToHash), confIP);

            switch (response)
            {
                case "OK":
                    {
                        addlog.Debug("Конфігурація на сервері ідентична!");
                        return;
                    }

                case "Must Reg":
                    addlog.Info("Такий Mover ще не зареєстрований!");
                    RegInServer();
                    break;

                default:
                    ApplyConfFromServer(response);
                    break;
            }

        }

        /// <summary>
        /// Registration Mover on Server
        /// </summary>
        private void RegInServer()
        {
            try
            {
                MoverWeb.MoverServ MW = new MoverWeb.MoverServ();

                string response = MW.REG(confIP, Environment.MachineName, Application.ExecutablePath, vers, Convert.ToInt32(chBAutoRun.Checked), Convert.ToInt32(Sec.Value), Convert.ToInt32(chBbaloon.Checked));
                if (response == "OK")
                {
                    addlog.Info("На сервері успішно зареєстровано Mover з ІР - {0}", confIP);
                }
                else
                {
                    addlog.Error("При реєстрації Mover виникла помилка на сервері: {0}", response);
                }
            }
            catch (Exception ex)
            {
                addlog.Error("При реєстрації Mover на сервері, виникла помилка: {0}", ex.Message);
            }
            
        }

        /// <summary>
        /// Load config from server to Mover
        /// </summary>
        /// <param name="response"></param>
        private void ApplyConfFromServer(string response)
        {
           //TODO: zapovnyty conf from server



        }

        private void chBconf_CheckedChanged(object sender, EventArgs e)
        {
            GetConfFromServer();
        }
    }
}
