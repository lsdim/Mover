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
using System.Xml;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using Ionic.Zip;
using IWshRuntimeLibrary;

namespace Mover
{
    public partial class MainF : Form
    {
        public string vers = new Version(Application.ProductVersion).ToString();
        private static Log addlog;// = new Log();
        private string urlUPD = "";
        private string confIP;
        public string IP;
        public string HashConf;

        System.Drawing.Point p = new Point();

        public MainF()
        {
            InitializeComponent();
        }

        public NotifyIcon GetNotifyIcon()
        {
            return nI1;
        }
        
        /// <summary>
        /// Download dlls for work
        /// </summary>
        private void GetDlls()
        {
            try
            {
                IniFiles ini = new IniFiles(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Properties.ini"));

                urlUPD = ini.ReadString("UPD", "source", "");
                if (urlUPD == "")
                {
                    ini.WriteString("UPD", "source", "http://10.202.14.15/mover/");
                    urlUPD = "http://10.202.14.15/mover/";
                }

                string[] files = { "NLog.dll", "NLog.config", "NLog.xml", "Ionic.Zip.dll" };//, "Mover.XmlSerializers.dll" };
                UpdateApl upd = new Mover.UpdateApl(urlUPD, false);
                upd.DownloadSystemFile(files);
            }
            catch(Exception ex)
            {

            }

        }


        private void MainF_Load(object sender, EventArgs e)
        {
            try
            {
                //GetDlls();

                addlog = new Log();

                addlog.Info("**--**--**Mover v{0}**--**--**", vers);
                addlog.Info("**--**--**Mover v{0}**--**--**", UpdateApl.GetChecksumm(Application.ExecutablePath));
                addlog.Info("***Програму запущенно {0}***", Application.ExecutablePath);


                //  MoverWeb.MoverServ ms = new MoverWeb.MoverServ();

                string guid = Marshal.GetTypeLibGuidForAssembly(Assembly.GetExecutingAssembly()).ToString();

                Mutex mutexObj = new Mutex(true, guid, out bool existed);

                if (!existed)
                {
                    addlog.Info(String.Format("Спроба подвійного запуску", vers));
                    Process.GetCurrentProcess().Kill();
                }

                GetLocalIP();
                ReadIni();

                chBAutoRun.Checked = System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + Path.GetFileNameWithoutExtension(Application.ExecutablePath) + ".lnk"); 

                    //get hash sum config
                    HashConf = GetConfHash();

                // Check and Get New version from server
                UpdateApl upd = new UpdateApl(urlUPD);
                //upd.Download();

                nI1.Text = "Mover server v." + vers;

                Thread TCPin = new Thread(GetFile)
                {
                    IsBackground = true
                };
                TCPin.Start();

                //start Timer for main work
                tMotor.Interval = (int)Sec.Value * 1000;
                tMotor.Enabled = true;


            }
            catch(Exception ex)
            {
                addlog.Error("При запуску Мвера виникла помилка: {0}", ex.Message);
            }
            
        }

        private void GetLocalIP()
        {
            try
            {
                IPAddress[] IPadr = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
                var IPlist = new List<string>();

                foreach (IPAddress ip in IPadr)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        IPlist.Add(ip.ToString());
                }

                if (IPlist.Count == 1)
                    IP = IPlist[0];
                else
                {
                    foreach (string ip in IPlist)
                    {
                        if (ip.StartsWith("10.")) //костиль для Пошти
                        {
                            IP = ip;
                            break;
                        }
                    }
                }

                if (IP == "")
                    IP = "127.0.0.1";

                addlog.Info("ІР адреса Mover: {0}", IP);
            }
            catch (Exception ex)
            {
                addlog.Error("При отриманні ІР адреси виникла помилка: {0}", ex.Message);
            }


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

            if (confIP == "0.0.0.0")
                confIP = IP;
            else
            {
                cBconf.Text = confIP;
                addlog.Debug("Конфігурація на сервері: {0}",confIP);
            }


            urlUPD = ini.ReadString("UPD", "source", "");
            if (urlUPD == "")
            {
                ini.WriteString("UPD", "source", "http://10.202.14.15/mover/");
                urlUPD = "http://10.202.14.15/mover/";
            }

            chBbaloon.Checked = ini.ReadBool("LOG", "ballon", true);
            chBconf.Checked = ini.ReadBool("CONF", "act", false);

            int confCount = ini.ReadInteger("CONF", "count", 0);
            cBconf.Items.Clear();
            for (int i = 1; i <= confCount; i++)
            {
                cBconf.Items.Add(ini.ReadString("CONF", "conmName." + i.ToString(),""));
            }

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
            if (this.WindowState != FormWindowState.Minimized)
                HashConf = GetConfHash();
        }

        private void SettingTSMI_Click(object sender, EventArgs e)
        {
            NormalMainForm();
            /*this.Show();
            this.WindowState = FormWindowState.Normal;
            tMotor.Enabled = false;*/
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
                NormalMainForm();
            }
        }

        private void NormalMainForm()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            HashConf = GetConfHash();
            tMotor.Enabled = false;
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
            if (GetConfHash() != HashConf)
            {
                DialogResult dlgRez = MessageBox.Show("Зберегти зміни? ", "Mover", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (dlgRez == DialogResult.Cancel)
                    return;
                if (dlgRez == DialogResult.Yes)
                {
                    Thread write = new Thread(WriteInvoke);
                    write.Start();

                }
                if (dlgRez == DialogResult.No)
                {
                    ReadIni();
                    BackgroundWorker bgw = new BackgroundWorker();
                    bgw.DoWork += BGW_GetConfFromServer_DoWork;
                    bgw.RunWorkerCompleted += BGW_GetConfFromServer_Completed;
                    bgw.RunWorkerAsync();
                }
            }

            this.Hide();
            this.WindowState = FormWindowState.Minimized;
            tMotor.Interval = (int)Sec.Value * 1000;
            tMotor.Enabled = true;
        }

        public delegate void SaveIni(); // delegat for new Thread for write ini file
        public delegate void SaveServ(); // delegat for new Thread for write into server
        private void WriteInvoke()
        {            
            SaveIni dlgtSave = new SaveIni(WriteIni);
            Invoke(dlgtSave);

            SaveServ dlgServ = new SaveServ(SaveCfgIntoServer);
            Invoke(dlgServ);
        }

        /// <summary>
        /// Write parametr into ini files
        /// </summary>
        private void WriteIni()
        {
            try
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

                if (chBconf.Checked && cBconf.Text != "")
                    ini.WriteString("MOOOVER", "IP", cBconf.Text);
                else
                {
                    ini.WriteString("MOOOVER", "IP", "0.0.0.0");
                    chBconf.Checked = false;
                }

                ini.WriteString("CONF", "act", Convert.ToInt32(chBconf.Checked).ToString());
                ini.WriteString("CONF", "count", cBconf.Items.Count.ToString());
                for (int i = 0; i < cBconf.Items.Count; i++)
                {
                    ini.WriteString("CONF", "conmName." + (i + 1).ToString(), cBconf.Items[i].ToString());
                }

                addlog.Info("Конфігурацію збереженно!");
            }
            catch(Exception ex)
            {
                addlog.Error("При збереженні конфігурації виникла помилка: {0}",ex.Message);
            }


        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            ReadIni();

            BackgroundWorker bgw = new BackgroundWorker();
            bgw.DoWork += BGW_GetConfFromServer_DoWork;
            bgw.RunWorkerCompleted += BGW_GetConfFromServer_Completed;
            bgw.RunWorkerAsync();

            this.Hide();
            this.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// BackGroundWorker DoWork for Write ini file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BGW_GetConfFromServer_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = GetConfFromServer();
        }

        private void BGW_GetConfFromServer_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    addlog.Error("При отриаманні кофігураці виникла помилка на сервері: {0}", e.Error.Message);
                    return;
                }

                string response = e.Result.ToString();

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

                    default: //check data or error
                        if (response.IndexOf("NewDataSet") > -1)
                            ApplyConfFromServer(response);
                        else
                            addlog.Error("При отриаманні кофігураці виникла помилка на сервері: {0}", response);
                        break;
                }
            }
            catch(Exception ex)
            {
                addlog.Error("При отриаманні кофігураці виникла помилка: {0}", ex.Message);
            }
            finally
            {
                foreach (Control cntrl in this.Controls)
                {
                    cntrl.Enabled = true;
                }
            }

                

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

        private string GetConfHash()
        {
            try
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

                return Hash(strToHash);
            }
            catch (Exception ex)
            {
                addlog.Error("При отрмані контрольної суми виникла помилка: {0}", ex.Message);
                return "";
            }
        }


        /// <summary>
        /// Check and get config from server
        /// </summary>
        private object GetConfFromServer()
        {
            try
            {
                /*
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
                */
                string hash = GetConfHash();
                if (hash == "")
                    return "";

                MoverWeb.MoverServ MW = new MoverWeb.MoverServ();                

                string response = MW.GetCnf(hash, confIP);

                return response;
                
            }
            catch (Exception ex)
            {
                addlog.Error("При отрмані конфігурації виникла помилка: {0}", ex.Message);
                return ex.Message;
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
            try
            {
                string[] xml = response.Split(new string[] { "</NewDataSet>" }, StringSplitOptions.RemoveEmptyEntries);
                
                string tmballon; // timer and baloon value
                if (xml.Count() != 3)
                {
                    GV1.Rows.Clear();
                    GV2.Rows.Clear();
                    tmballon = "<New>" + xml[0] + "</New>";
                }
                else
                {
                    xml[0] += "</NewDataSet>";
                    xml[1] += "</NewDataSet>";
                    tmballon = "<NewDataSet>" + xml[2] + "</NewDataSet>";

                    ConfFromXML(xml[0], GV1);
                    ConfFromXML(xml[1], GV2);
                }

                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(tmballon);

                if (int.TryParse(xmldoc.GetElementsByTagName("INTERVAL")[0].InnerText, out int time))
                    Sec.Value = time;
                if (int.TryParse(xmldoc.GetElementsByTagName("BALOON")[0].InnerText, out int baloon))
                    chBbaloon.Checked = Convert.ToBoolean(baloon);

                WriteIni();
                addlog.Info("Успішно отримано конфігурацію \"{0}\" із сервера", confIP);
            }
            catch(Exception ex)
            {
                addlog.Error("При отриманні конфігурації виникла помилка: {0}", ex.Message);
            }

        }

        private void ConfFromXML(string xml, DataGridView GV)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(xml);

                GV.Rows.Clear();

                XmlElement elem = xmldoc.DocumentElement;
                foreach (XmlNode node in elem)
                {
                    if (node.HasChildNodes)
                    {
                        int i = GV.Rows.Add();
                        GV.Rows[i].HeaderCell.Value = (i + 1).ToString();
                        int j = 0;

                        foreach (XmlNode chNode in node)
                        {
                            GV.Rows[i].Cells[j].Value = chNode.InnerText;
                            j++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                addlog.Error("При заповненні даними із сервера виникла помилка: {0}", ex.Message);
            }
        }
            

        private void chBconf_CheckedChanged(object sender, EventArgs e)
        {
            cBconf.Visible = chBconf.Checked;
            bAddConf.Visible = chBconf.Checked;
            bRemConf.Visible = chBconf.Checked;
            //GetConfFromServer();
        }

        private void cBconf_SelectedIndexChanged(object sender, EventArgs e)
        {
            confIP = cBconf.Text.Trim();
            if (confIP != "")
            {
                foreach (Control cntrl in this.Controls)
                {
                    cntrl.Enabled = false;
                }

                BackgroundWorker bgw = new BackgroundWorker();
                bgw.DoWork += BGW_GetConfFromServer_DoWork;
                bgw.RunWorkerCompleted += BGW_GetConfFromServer_Completed;
                bgw.RunWorkerAsync();
                
            }
                //GetConfFromServer();
            else
                confIP = IP;
        }

        private void bAddConf_Click(object sender, EventArgs e)
        {
            bool unic = true;

            foreach (string item in cBconf.Items)
            {
                if (item.ToString() == cBconf.Text)
                {
                    cBconf.SelectedItem = item;
                    unic = false;
                    break;
                }
            }

            if (unic)
            {
                int ind = cBconf.Items.Add(cBconf.Text);
                cBconf.SelectedIndex = ind;

            }
        }

        private void bRemConf_Click(object sender, EventArgs e)
        {
            cBconf.Items.Remove(cBconf.SelectedItem);
            if (cBconf.Items.Count > 0)
            {
                cBconf.SelectedItem = cBconf.Items[0];
            }
        }

        private void SaveCfgIntoServer()
        {
            try
            {
                string dir_id = "";
                string MC = "";
                string mask = "";
                string stat = "";
                string dir = "";

                foreach (DataGridViewRow row in GV1.Rows)
                {
                    if (dir_id == "")
                        dir_id = row.Cells[0].Value.ToString();
                    else
                        dir_id += "^" + row.Cells[0].Value.ToString();

                    if (MC == "")
                        MC = row.Cells[1].Value.ToString();
                    else
                        MC += "^" + row.Cells[1].Value.ToString();

                    if (mask == "")
                        mask = row.Cells[2].Value.ToString();
                    else
                        mask += "^" + row.Cells[2].Value.ToString();

                    if (stat == "")
                        stat = row.Cells[3].Value.ToString();
                    else
                        stat += "^" + row.Cells[3].Value.ToString();
                }
                foreach (DataGridViewRow row in GV2.Rows)
                {
                    if (dir == "")
                        dir = row.Cells[0].Value.ToString();
                    else
                        dir += "^" + row.Cells[0].Value.ToString();
                }



                MoverWeb.MoverServ MW = new MoverWeb.MoverServ();
                string response = MW.SetCnf(dir_id, MC, mask, stat, dir, confIP, Convert.ToInt32(Sec.Value), Convert.ToInt32(chBbaloon.Checked));

                IniFiles ini = new IniFiles(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Properties.ini"));

                switch (response)
                {
                    case "OK":
                        {
                            addlog.Debug("Конфігурацію на сервері оновленно!");                            
                            ini.WriteString("UPD", "Save", "0");                            
                            return;
                        }

                    case "Must Reg":
                        addlog.Info("Такий Mover ще не зареєстрований!");
                        RegInServer();
                        break;

                    default: // error                        
                        addlog.Error("При збережені кофігурації виникла помилка на сервері: {0}", response);                       
                        ini.WriteString("UPD", "Save", "1");
                        break;
                }

            }
            catch(Exception ex)
            {
                addlog.Error("При збереженні конфігурації на сервер виникла помилка: {0}", ex.Message);
                IniFiles ini = new IniFiles(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Properties.ini"));
                ini.WriteString("UPD", "Save", "1");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MoverWork mv = new MoverWork(GV2[0, 0].Value.ToString(), GV1[1, 0].Value.ToString(), GV1[2, 0].Value.ToString(), (MoverWork.Operation)6);
            //mv.Run();
            Motor();
        }

        private void GetFile()
        {
            try
            {
                TcpListener clientListener = new TcpListener(IPAddress.Any, 30001);
                clientListener.Start();
                while (true)
                {
                    TcpClient client = clientListener.AcceptTcpClient();
                    client.ReceiveTimeout = 1000;
                    NetworkStream readerStream = client.GetStream();
                    BinaryFormatter outformat = new BinaryFormatter();

                    addlog.Info("початок приймання файлів по ТСР");
                    string[] lng_pth = outformat.Deserialize(readerStream).ToString().Split('|'); // get length file and path destination

                    Directory.CreateDirectory(lng_pth[1]);

                    string nameFile = Path.Combine(lng_pth[1], client.Client.RemoteEndPoint.ToString().Split(':')[0] + DateTime.Now.ToString("_yyyyMMddHHmmss") + ".zip");

                    FileStream fs = new FileStream(nameFile, FileMode.OpenOrCreate);
                    BinaryWriter bw = new BinaryWriter(fs);
                    try
                    {
                        int count;
                        count = int.Parse(lng_pth[0]);//Получаем размер файла

                        int i = 0;
                        for (; i < count; i += 1280)//Цикл пока не дойдём до конца файла
                        {

                            byte[] buf = (byte[])(outformat.Deserialize(readerStream));//Собственно читаем из потока и записываем в файл
                            bw.Write(buf);
                        }

                        /*
                        fs.Seek(0, SeekOrigin.Begin);
                        //Check check Sum File
                        string sum = UpdateApl.GetChecksumm(fs);
                        if (lng_pth[3] == sum)
                            outformat.Serialize(readerStream, "OK");
                        else
                        {
                            outformat.Serialize(readerStream, "-1");
                        }
*/
                        addlog.Info("Отримано файл(и) від - {0}", client.Client.RemoteEndPoint.ToString().Split(':')[0]);

                        ExtractExistingFileAction owerwrite;
                        if (lng_pth[2] == "1")
                            owerwrite = ExtractExistingFileAction.OverwriteSilently;
                        else
                            owerwrite = ExtractExistingFileAction.DoNotOverwrite;



                        ExtractZip(nameFile, owerwrite);//Extract responsed zip file

                    }
                    catch (Exception ex)
                    {
                        addlog.Error("При отриманні файлів виникла помилка: {0}", ex.Message);
                    }
                    finally
                    {
                        bw.Close();
                        fs.Close();
                        System.IO.File.Delete(nameFile);
                    }
                    

                }
            }
            catch (Exception ex)
            {
                addlog.Error("При отриманні файлів по ТСР виникла помилка: {0}", ex.Message);
            }
        }

        private void ExtractZip(string nameFile, ExtractExistingFileAction owerwrite)
        {
            ZipFile zip = ZipFile.Read(nameFile);
            zip.AlternateEncoding = Encoding.UTF8;

            zip.ExtractProgress += ExtractProgress;           

            zip.ExtractAll(Path.GetDirectoryName(nameFile), owerwrite);
            zip.Dispose();            
        }

        private void ExtractProgress(object sender, ExtractProgressEventArgs e)
        {
            if (e.EventType == ZipProgressEventType.Extracting_BeforeExtractEntry)
                addlog.Info("{0}{1}", e.ExtractLocation, e.CurrentEntry.FileName);

        }

        private void chBAutoRun_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string fname = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + Path.GetFileNameWithoutExtension(Application.ExecutablePath) + ".lnk";
                if (chBAutoRun.Checked)
                {
                    if (!System.IO.File.Exists(fname))
                    {
                        CreateShortCut();
                        addlog.Info("Додано ярлик в автозавантаження");
                    }
                }
                else
                {
                    if (System.IO.File.Exists(fname))
                    {
                        System.IO.File.Delete(fname);
                        addlog.Info("Видалено ярлик з автозавантаження");
                    }
                }
            }
            catch (Exception ex)
            {
                addlog.Error("При створенні або видалені ярлика виникла помилка: {0}", ex.Message);
            }
        }

        private void CreateShortCut()
        {
            try
            {
                WshShell shell = new WshShell();
                string adr = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + Path.GetFileNameWithoutExtension(Application.ExecutablePath) + ".lnk";//shell.SpecialFolders.Item(ref startup) + "movertest.lnk";
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(adr);
                shortcut.Description = "Mover";
                shortcut.TargetPath = Application.ExecutablePath;
                shortcut.Save();
            }
            catch (Exception ex)
            {
                addlog.Error("При створені архіву виникла помилка: {0}", ex.Message);
            }
        }

        private void GV1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            p.Y = e.RowIndex;
            p.X = e.ColumnIndex;
        }

        private void CopyNext_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GV1.Rows.Insert(p.Y + 1, GV1.Rows[p.Y].Cells[0].Value, GV1.Rows[p.Y].Cells[1].Value, GV1.Rows[p.Y].Cells[2].Value, GV1.Rows[p.Y].Cells[3].Value);
                GV1.CurrentCell = GV1.Rows[p.Y + 1].Cells[0];
                for (int i = p.Y; i < GV1.RowCount; i++)
                    GV1.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
            catch (Exception ex)
            {
                addlog.Error("При копіюванні рядка виникла помилка: {0}", ex.Message);
            }
        }

        private void CopyEnd_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GV1.Rows.Add(GV1.Rows[p.Y].Cells[0].Value, GV1.Rows[p.Y].Cells[1].Value, GV1.Rows[p.Y].Cells[2].Value, GV1.Rows[p.Y].Cells[3].Value);
                GV1.Rows[GV1.Rows.Count - 1].HeaderCell.Value = GV1.Rows.Count.ToString();
                GV1.CurrentCell = GV1.Rows[GV1.Rows.Count - 1].Cells[0];
            }
            catch (Exception ex)
            {
                addlog.Error("При копіюванні рядка виникла помилка: {0}", ex.Message);
            }
        }

        private void Motor()
        {
            try
            {
                List<string> conf = new List<string>();
                foreach (DataGridViewRow row in GV1.Rows)
                {
                    string tmp = "";

                    if (row.Cells[3].Value.ToString() != "0")
                    {

                        string[] dir = row.Cells[0].Value.ToString().Split(',');
                        foreach (string d in dir)
                        {
                            if (int.Parse(d.Trim()) != 0)
                            {
                                if (tmp == "")
                                    tmp = GV2.Rows[int.Parse(d.Trim()) - 1].Cells[0].Value.ToString();
                                else
                                    tmp += "|" + GV2.Rows[int.Parse(d.Trim()) - 1].Cells[0].Value.ToString();
                            }
                        }

                        if (tmp != "")
                        {
                            for (int i = 1; i < 4; i++)
                                tmp += "§" + row.Cells[i].Value.ToString(); //ALT+789

                            conf.Add(tmp);
                        }
                    }
                }

                prB1.Visible = true;
                prB1.Maximum = conf.Count;

                foreach (Control cntrl in this.Controls)
                {
                    cntrl.Enabled = false;
                }


                BackgroundWorker bgw = new BackgroundWorker();
                bgw.WorkerReportsProgress = true;
                bgw.ProgressChanged += Bgw_Motor_ProgressChanged;
                bgw.DoWork += BGW_Motor_DoWork;
                bgw.RunWorkerCompleted += Bgw_Motor_RunWorkerCompleted;
                bgw.RunWorkerAsync(conf);

            }
            catch (Exception ex)
            {
                addlog.Error("При роботі виникла помилка: {0}", ex.Message);
            }


        }

        private void Bgw_Motor_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                prB1.Value = 0;
                prB1.Visible = false;
            }
            catch (Exception ex)
            {
                addlog.Error("При завершені роботи виникла помилка: {0}", ex.Message);
            }
            finally
            {
                foreach (Control cntrl in this.Controls)
                {
                    cntrl.Enabled = true;
                }
            }
        }

        private void Bgw_Motor_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                prB1.Value = e.ProgressPercentage;
            }
            catch (Exception ex)
            {
                addlog.Error("При зміні значення прогресу виконання виникла помилка: {0}", ex.Message);
            }
        }

        private void BGW_Motor_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker bgw = sender as BackgroundWorker;
                List<string> rows = (List<string>)e.Argument;

                //int step = 100 / rows.Count;
                int i = 1;
                foreach (string row in rows)
                {
                    string[] conf = row.Split('§'); //alt+789;

                    MoverWork mw = new MoverWork(conf[0], conf[1], conf[2], (MoverWork.Operation)int.Parse(conf[3]));
                    mw.Run();                    

                    bgw.ReportProgress(i);
                    i++;
                }

                //bgw.ReportProgress(100);

            }
            catch (Exception ex)
            {
                addlog.Error("При роботі в потоці виникла помилка: {0}", ex.Message);
            }



        }

        private void tMotor_Tick(object sender, EventArgs e)
        {
            Motor();
        }
    }
}

