using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Xml;
using System.Windows.Forms;
using System.ComponentModel;

namespace Mover
{
    class UpdateApl
    {        

        private string nameFile = Application.ExecutablePath;
        string new_name = Path.GetFileNameWithoutExtension(Application.ExecutablePath) + ".upd";
        private string url;
        private string cSum = "";
        private string updcSum = "";
        private string updName = "updater.exe";

        private static Log addlog = new Log();

        public UpdateApl(string _url)
        {
            url = _url;
        }
                

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            if (Checksumm(new_name, cSum))
            {
                try
                {
                    Process.Start(updName, new_name + " " + Path.GetFileName(nameFile));
                    Process.GetCurrentProcess().Kill();
                }
                catch (Exception ex)
                {
                    addlog.Debug(ex.Message);
                }
            }
            else
            {
                Download();
            }
        }

        private void UPDCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (!Checksumm(updName, updcSum))
                DownloadUpd(updcSum);
            else
                addlog.Debug("\"{0}\" успішно завантажено", updName);           
        }

        public static string GetChecksumm(string filename)
        {
            using (FileStream fs = File.OpenRead(filename))
            {
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] fileData = new byte[fs.Length];
                fs.Read(fileData, 0, (int)fs.Length);
                byte[] checkSumm = md5.ComputeHash(fileData);
                return BitConverter.ToString(checkSumm).Replace("-", String.Empty);
            }
        }

        private bool Checksumm(string filename, string summ)
        {
            return GetChecksumm(filename) == (summ).ToUpper() ? true : false;
            /*using (FileStream fs = File.OpenRead(filename))
            {
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] fileData = new byte[fs.Length];
                fs.Read(fileData, 0, (int)fs.Length);
                byte[] checkSumm = md5.ComputeHash(fileData);                
                return BitConverter.ToString(checkSumm).Replace("-", String.Empty) == (summ).ToUpper() ? true : false;
            } */
        }


        private void DownloadUpd(string updcSum)
        {
            try
            {
                if (!File.Exists(updName) || !Checksumm(updName, updcSum))
                {
                    addlog.Debug("Не знайдено \"{0}\", або різні контрольні суми, спроба завантажити із сервера", updName);

                    if (File.Exists(updName)) { File.Delete(updName); }

                    WebClient client = new WebClient();
                    client.DownloadFileCompleted += new AsyncCompletedEventHandler(UPDCompleted);
                    client.DownloadFileAsync(new Uri(url + "/" + updName), updName);                    
                }  
                
            }
            catch (Exception ex)
            {
                addlog.Error("Помилка при завантаженні модуля для оновлення: {0}", ex.Message);
            }

        }

        public void Download()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(url+"/version.xml");
               // XmlElement elem = doc.DocumentElement;

                string mVers = doc.GetElementsByTagName("mover")[0].InnerText; 
               /* foreach (XmlNode node in elem)
                {
                    if (node.Name.ToUpper() =="Mover".ToUpper())
                    {
                        mVers = node.InnerText;
                        break; 
                    }
                    
                }
                */
               

               // string a = elem..Item(1).InnerText;

                Version remoteVersion = new Version(mVers);
                Version localVersion = new Version(Application.ProductVersion);

                cSum = doc.GetElementsByTagName("controlSum")[0].InnerText;
                updcSum = doc.GetElementsByTagName("UPDcontrolSum")[0].InnerText;


                if (localVersion < remoteVersion)
                {
                    addlog.Info("Знайдено нову версію Mover - \"{0}\"", remoteVersion.ToString());
                    DownloadUpd(updcSum);

                    if (File.Exists(new_name)) { File.Delete(new_name); }

                    WebClient client = new WebClient();
                    //client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                    client.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                    client.DownloadFileAsync(new Uri(url + "/mover.exe"), new_name);
                }
                else
                    addlog.Info("Версія програми актуальна");
            }
            catch (Exception ex)
            {
                addlog.Error("Помилка при завантаженні оновлення: {0}", ex.Message);
            }
        }
    }
}
