﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Ionic;
using System.Linq;
using Ionic.Zip;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace Mover
{
    public class MoverWork
    {
        public enum Operation
        {
            Copy = 11,
            CopyRepl = 1,
            Move = 21,
            MoveRepl = 2,
            Delete = 3,
            Run = 4,
            ComandProm = 5,
            Message = 6,
            Rename = 7,
            DeleteExcept = 8
        }

        /// <summary>
        /// Struct for mowerWork object
        /// </summary>
        public struct MaskRez
        {
            public string mask;
            public string clear_mask;
            public bool TCP;
            public bool Recurcia;
            public TimeSpan ttm1;
            public TimeSpan ttm2;
            public bool[] days;

            public bool IsEveryDay()
            {
                foreach (bool day in days)
                    if (day) return false;

                return true;
            }

            public string GetIP()
            {
                try
                {
                    string rez = "";
                    // if (TCP)
                    rez = clear_mask.Substring(2, clear_mask.IndexOf('\\', 2) - 2);
                    if (IPAddress.TryParse(rez, out IPAddress ip))
                        return rez;
                    else
                        return "";
                }
                catch(Exception ex)
                {
                    addlog.Error("Помилка при отриманні ІР адреси із шляху: {0}", ex.Message);
                    return "";
                }

            }

            public string GetPath()
            {
                try
                {
                    string rez = "";
                    //  if (TCP)
                    rez = clear_mask.Substring(clear_mask.IndexOf('\\', 2) + 1, (clear_mask.Length - clear_mask.IndexOf('\\', 2)-1));

                    return rez;
                }
                catch(Exception ex)
                {
                    addlog.Error("Помилка при отримані шляху без ІР адреси: {0}",ex.Message);
                    return "";
                }
                

            }
        }

        
       

        /// <summary>
        /// Loging all events
        /// </summary>
        private static Log addlog = new Log();

        private string dir_from;
        private string dir_destin;
        private string mask;
        private Operation oper;

        /// <summary>
        /// Create new object MoverWork
        /// </summary>
        /// <param name="_dir_from">dir for scan (full path may bе many separated by symbol "|")</param>
        /// <param name="_dir_destin">dir or command prom or application or message or old_string%new_string &lt;*>&lt;.*>&lt;*.> for rename </param>
        /// <param name="_mask">mask for search (separated by symbol ";")</param>
        /// <param name="_oper">operation from Operation typr</param>
        public MoverWork(string _dir_from, string _dir_destin, string _mask, Operation _oper)
        {
            try
            {
                dir_from = _dir_from;
                dir_destin = _dir_destin;
                mask = _mask;
                oper = _oper;
            }
            catch (Exception ex)
            {
                addlog.Debug(ex.Message);
            }
        }

        public bool InTime(MaskRez elem)
        {
            bool rez = false;
            //TODO : Check TIME
            if (elem.ttm1 != new TimeSpan(0))
            {
                TimeSpan now = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                int t = now.CompareTo(elem.ttm1);
                TimeSpan r = elem.ttm1 - now;
                // if (now.CompareTo(elem.ttm1) == 0)
                rez = true;

            }

            return rez;
        }

        //Main function
        /// <summary>
        /// Main function that work all job
        /// </summary>
        public void Run()
        {
            //Example MoverWork mw = new MoverWork(@"D:\\11111\\22222\\", GV2[1, GV2.Rows.Count - 1].Value.ToString(), "vedo3.XLs;*.det ", MoverWork.Operation.DeleteExcept);

            MaskRez elem = Mask(mask);

            
            if (!elem.IsEveryDay())
            {
                if (!elem.days[(int)DateTime.Today.DayOfWeek])
                    return;
            }

            //TODO : Check TIME
            if (!InTime(elem))
                return;




            SearchOption recur = elem.Recurcia ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            string pattern = elem.clear_mask;

            string[] patterns = pattern.Split(';');
            

            dir_from = Mask(dir_from).clear_mask;
            string[] dirs_from = dir_from.Split('|');

            for(int i=0; i<dirs_from.Length; i++)
                dirs_from[i] = (dirs_from[i][dirs_from[i].Length-1] == '\\') ? dirs_from[i] : dirs_from[i] + "\\";

            dir_destin = Mask(dir_destin).clear_mask;

            if (oper == Operation.Copy || oper == Operation.CopyRepl || oper == Operation.Move || oper == Operation.MoveRepl)
                dir_destin = (dir_destin[dir_destin.Length-1] == '\\') ? dir_destin : dir_destin + "\\";


            IEnumerable<string> files = SafeEnumerateFiles(dirs_from, patterns, recur);

            if (files.Count() == 0)
                return;
                        
            
            switch (oper)
            {
                case Operation.Copy:
                    if (elem.TCP)
                        CopyFileTCP(Mask(dir_destin).GetIP(), Mask(dir_destin).GetPath(), files, false);
                    else
                        CopyFile(dir_destin, files, false);
                    break;
                case Operation.CopyRepl:
                    if (elem.TCP)
                        CopyFileTCP(Mask(dir_destin).GetIP(), Mask(dir_destin).GetPath(), files, true);
                    else
                        CopyFile(dir_destin, files, true);
                    break;
                case Operation.Move:
                    if (elem.TCP)
                        MoveFileTCP(Mask(dir_destin).GetIP(), Mask(dir_destin).GetPath(), files, false);
                    else
                        MoveFile(dir_destin, files, false);
                    break;
                case Operation.MoveRepl:
                    if (elem.TCP)
                        MoveFileTCP(Mask(dir_destin).GetIP(), Mask(dir_destin).GetPath(), files, true);
                    else
                        MoveFile(dir_destin, files, true);
                    break;
                case Operation.Delete:
                    DeleteFile(files);
                    break;
                case Operation.Run:
                    RunFile(dir_destin, files);
                    break;
                case Operation.ComandProm:
                    RunCMD(dir_destin, files);
                    break;
                case Operation.Message:
                    Mess(dir_destin, files);
                    break;
                case Operation.Rename:
                    Rename(dir_destin, files);
                    break;
                case Operation.DeleteExcept:
                    DeleteExc(files, dirs_from, recur);
                    break;
            }     
        }

        /// <summary>
        /// Coping files
        /// </summary>
        /// <param name="dectination">destinations path</param>
        /// <param name="files">files list</param>
        /// <param name="owerwrite"> owerwrite</param>
        private void CopyFile(string dectination, IEnumerable<string> files, bool owerwrite)
        {
            try
            {
                Directory.CreateDirectory(dectination);
                foreach (string f in files)
                {
                    try
                    {
                        File.Copy(f, Path.Combine(dectination, Path.GetFileName(f)), owerwrite);
                        addlog.Info("Скопійовано файл \"{0}\" до \"{1}\"", f, Path.Combine(dectination, Path.GetFileName(f)));
                    }
                    catch (Exception ex)
                    {
                        addlog.Error("При копіюванні файлу \"{0}\" виникла помилка: \"{1}\"", f, ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                addlog.Error("При копіюванні до \"{0}\" виникла помилка: {0}", dectination, ex.Message);//error CreaTE dIRECTORY                
            }
        }

        /// <summary>
        /// Moving files
        /// </summary>
        /// <param name="dectination">dectination</param>
        /// <param name="files">files list</param>
        /// <param name="owerwrite">owerwrite</param>
        private void MoveFile(string dectination, IEnumerable<string> files, bool owerwrite)
        {
            try
            {

                Directory.CreateDirectory(dectination);
                foreach (string f in files)
                {
                    try
                    {
                        if (owerwrite)
                            if (File.Exists(Path.Combine(dectination, Path.GetFileName(f))))
                                File.Delete(Path.Combine(dectination, Path.GetFileName(f)));
                        File.Move(f, Path.Combine(dectination, Path.GetFileName(f)));
                        addlog.Info("Переміщено файл \"{0}\" до \"{1}\"", f, Path.Combine(dectination, Path.GetFileName(f)));
                    }
                    
                    catch (Exception ex)
                    {
                        addlog.Error("При переміщені файлу \"{0}\" виникла помилка: {1}", f,ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                addlog.Error("При переміщені до \"{0}\" виникла помилка: {1}", dectination, ex.Message);//error CreaTE dIRECTORY
            }
        }

        /// <summary>
        /// Deleting files
        /// </summary>
        /// <param name="files"> files list</param>
        private void DeleteFile(IEnumerable<string> files)
        {               
            foreach (string f in files)
             {
                    try
                    {                        
                        File.Delete(f);
                    addlog.Info("Видалено файл \"{0}\"", f);
                }

                    catch (Exception ex)
                    {
                       addlog.Error("При видалені файлу \"{0}\" виникла помилка: \"{1}\"", f,ex.Message);
                    }
             }   
            
        }

        /// <summary>
        /// Run application
        /// </summary>
        /// <param name="resource"> application or any file that will be opened</param>
        /// <param name="files">files list</param>
        private void RunFile(string resource, IEnumerable<string> files)
        {
            foreach (string f in files)
            {
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo(resource);
                    startInfo.WorkingDirectory = Path.GetDirectoryName(startInfo.FileName);
                    Process.Start(startInfo);
                    addlog.Info("Знайдено файл \"{0}\" і запущено \"{1}\"", f, resource);
                    break;
                    
                }
                catch(Exception ex)
                {
                    addlog.Error("При запуску \"{0}\" виникла помилка: \"{1}\"", resource, ex.Message);
                }
            }
        }

        /// <summary>
        /// Run command prom
        /// </summary>
        /// <param name="commands">command for run</param>
        /// <param name="files">files list</param>
        private void RunCMD(string commands, IEnumerable<string> files)
        {
            foreach (string f in files)
            {
                try
                {
                    Process process = new Process();
                    ProcessStartInfo startInfo = new ProcessStartInfo()
                    {
                        WindowStyle = ProcessWindowStyle.Hidden,
                        FileName = "cmd.exe",
                        Arguments = "/c " + commands
                    };
                     process.StartInfo = startInfo;
                     process.Start();

                    addlog.Info("Знайдено файл \"{0}\" і виконано \"{1}\"", f, commands);
                    break;
                }
                catch (Exception ex)
                {
                    addlog.Error("При виконанні \"{0}\" виникла помилка: \"{1}\"", commands, ex.Message);
                }
            }
        }

        /// <summary>
        /// Show message
        /// </summary>
        /// <param name="messText">Text for message</param>
        /// <param name="files">files list</param>
        private void Mess(string messText, IEnumerable<string> files)
        {
            foreach (string f in files)
            {
                try
                {
                    System.Threading.Thread mess = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(ShowMess));
                    mess.Start(messText);
                    addlog.Info("Знайдено файл \"{0}\" і показано повідомлення \"{1}\"", f, messText);
                    break;
                }
                catch (Exception ex)
                {
                    addlog.Error("При відображенні повідомлення \"{0}\" виникла помилка: \"{1}\"", messText, ex.Message);
                }
            }
        }

        /// <summary>
        /// Show message in new thread
        /// </summary>
        /// <param name="messText">Text for message</param>
        private void ShowMess(object messText)
        {
            try
            {
                System.Windows.Forms.MessageBox.Show((string)messText, "Mover", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                addlog.Error("При відображенні повідомлення \"{0}\" виникла помилка: \"{1}\"", messText, ex.Message);
            }
        }

        /// <summary>
        /// Rename files "old_chars%new_chars" (can use a mask &lt;*> or &lt;*.> or &lt;.*> as old_chars)
        /// </summary>
        /// <param name="text">template for rename</param>
        /// <param name="files">files list</param>
        private void Rename(string text, IEnumerable<string> files)
        {
            
                string[] name = text.Split('%');

                foreach (string f in files)
                {
                try
                {
                    switch (name[0].Trim())
                    {
                        case "<*>":
                            File.Move(f, Path.Combine(Path.GetDirectoryName(f), name[1].Trim()));
                            addlog.Info("Перейменованно файл \"{0}\" на \"{1}\"", f, name[1].Trim());
                            break;
                        case "<*.>":
                            File.Move(f, Path.Combine(Path.GetDirectoryName(f), name[1].Trim()+Path.GetExtension(f)));
                            addlog.Info("Перейменованно файл \"{0}\" на \"{1}\"", f, name[1].Trim() + Path.GetExtension(f));
                            break;
                        case "<.*>":
                            File.Move(f, Path.Combine(Path.GetDirectoryName(f),  Path.GetFileNameWithoutExtension(f)+"."+ name[1].Trim()));
                            addlog.Info("Перейменованно файл \"{0}\" на \"{1}\"", f, Path.GetFileNameWithoutExtension(f) + "." + name[1].Trim());
                            break;
                        default:
                            string new_name1 = Path.GetDirectoryName(f);
                            string new_name = Path.GetFileName(f).Replace(name[0].Trim(), name[1].Trim());
                            File.Move(f, Path.Combine(Path.GetDirectoryName(f), new_name));
                            addlog.Info("Перейменованно файл \"{0}\" на \"{1}\"", f, new_name);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    addlog.Error("При перейменуванні файлу \"{0}\" виникла помилка: \"{1}\"", f, ex.Message);
                }
            }

            
        }

        //delete all files except
        /// <summary>
        /// delete all except mentioned
        /// </summary>
        /// <param name="files">file list</param>
        /// <param name="dir">array of dirs for scan)</param>
        /// <param name="recur">SearchOption</param>
        private void DeleteExc(IEnumerable<string> files, string[] dir, SearchOption recur)
        {
            IEnumerable<string> all_files = SafeEnumerateFiles(dir, "*".Split(';'), recur);

            List<string> files_to_del = all_files.ToList<string>();

            foreach (string allf in all_files)
            foreach (string f in files)
            {
                try
                {
                        if (f == allf)
                        {
                            files_to_del.Remove(f);
                            break;
                        }                    
                }
                catch (Exception ex)
                {
                    addlog.Error("Виникла помилка при формуванні списку для видалення файлів: \"{0}\"", ex.Message);
                }                   
            }

            foreach (string f in files_to_del)
            {
                try
                {
                    File.Delete(f);
                    addlog.Info("Видалено файл \"{0}\"", f);
                }

                catch (Exception ex)
                {
                    addlog.Error("При видаленні файлу \"{0}\" виникла помилка: \"{1}\"", f, ex.Message);
                }
            }
        }

        //Create List of Files (search files by pattern)
        /// <summary>
        /// Search Files by patterns
        /// </summary>
        /// <param name="paths">array of dirs for search</param>
        /// <param name="searchPattern"> array of pattern for search</param>
        /// <param name="searchOption">SearchOption (Recur or not)</param>
        /// <returns></returns>
        private static IEnumerable<string> SafeEnumerateFiles(string[] paths, string[] searchPattern, SearchOption searchOption) 
        {             
            Stack<string> dirs = new Stack<string>();

            foreach (string path in paths)
            {
                dirs.Push(path);
            }

            while (dirs.Count > 0)
            {
                string currentDirPath = dirs.Pop();
                if (searchOption == SearchOption.AllDirectories)
                {
                    try
                    {
                        string[] subDirs = Directory.GetDirectories(currentDirPath);
                        foreach (string subDirPath in subDirs)
                        {
                            dirs.Push(subDirPath);
                        }
                    }
                    catch (UnauthorizedAccessException)
                    {
                        continue;
                    }
                    catch (DirectoryNotFoundException)
                    {
                        continue;
                    }
                }

                string[] files = null;
                try
                {
                    foreach (string pattern in searchPattern)
                    {
                        string[] tmpfiles = null;
                        tmpfiles = Directory.GetFiles(currentDirPath, pattern.Trim());
                        if (tmpfiles != null)
                        {
                            if (files == null)
                                files = tmpfiles;
                            else
                                files = files.Concat(tmpfiles).ToArray();
                        }
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    continue;
                }
                catch (DirectoryNotFoundException)
                {
                    continue;
                }

                foreach (string filePath in files)
                {
                    yield return filePath;
                }
            }
            
        }

        //analisis Field Mask (replace all patterns)
        /// <summary>
        /// analisis Field Mask (replace all patterns)
        /// </summary>
        /// <param name="text">in string with patterns</param>
        /// <returns>string with replaced patterns</returns>
        public static MaskRez Mask(string text)
        {
            
                MaskRez rez = new MaskRez();
            try
            {
                string mask = text;

                Regex regex = new Regex(@".*?(<\d{2}:\d{2}>).*?"); //get time for work
                MatchCollection mc = regex.Matches(text);
                switch (mc.Count)
                {
                    case 0:
                        break;
                    case 1:
                        TimeSpan.TryParse(mc[0].Groups[1].Value.Replace("<", "").Replace(">", "").Trim(), out rez.ttm1);
                        mask = mask.Replace(mc[0].Groups[1].Value, "");
                        break;
                    default: //case 2:
                        TimeSpan.TryParse(mc[0].Groups[1].Value.Replace("<", "").Replace(">", "").Trim(), out TimeSpan start);
                        TimeSpan.TryParse(mc[1].Groups[1].Value.Replace("<", "").Replace(">", "").Trim(), out TimeSpan end);
                        if (start > end)
                        {
                            end = start;
                            start = TimeSpan.Parse(mc[1].Groups[1].Value.Replace("<", "").Replace(">", "").Trim());
                        }
                        rez.ttm1 = start;
                        rez.ttm2 = end;
                        foreach (Match m in mc)
                            mask = mask.Replace(m.Groups[1].Value, "");
                        break;
                }

                rez.TCP = (text.IndexOf("<TCP>") >= 0) ? true : false;
                mask = mask.Replace("<TCP>", "");

                rez.Recurcia = (text.IndexOf("<RECUR>") >= 0) ? true : false;
                mask = mask.Replace("<RECUR>", "");


                bool[] day = new bool[7]; //get day of week for work

                day[(int)DayOfWeek.Monday] = (text.IndexOf("<пн>", StringComparison.InvariantCultureIgnoreCase) >= 0) ? true : false;
                day[(int)DayOfWeek.Tuesday] = (text.IndexOf("<вт>", StringComparison.InvariantCultureIgnoreCase) >= 0) ? true : false;
                day[(int)DayOfWeek.Wednesday] = (text.IndexOf("<ср>", StringComparison.InvariantCultureIgnoreCase) >= 0) ? true : false;
                day[(int)DayOfWeek.Thursday] = (text.IndexOf("<чт>", StringComparison.InvariantCultureIgnoreCase) >= 0) ? true : false;
                day[(int)DayOfWeek.Friday] = (text.IndexOf("<пт>", StringComparison.InvariantCultureIgnoreCase) >= 0) ? true : false;
                day[(int)DayOfWeek.Saturday] = (text.IndexOf("<сб>", StringComparison.InvariantCultureIgnoreCase) >= 0) ? true : false;
                day[(int)DayOfWeek.Sunday] = (text.IndexOf("<нд>", StringComparison.InvariantCultureIgnoreCase) >= 0) ? true : false;

                rez.days = day;

                mask = mask.ToUpper().Replace("<пн>".ToUpper(), "").Replace("<вт>".ToUpper(), "")
                    .Replace("<ср>".ToUpper(), "").Replace("<чт>".ToUpper(), "").Replace("<пт>".ToUpper(), "")
                    .Replace("<сб>".ToUpper(), "").Replace("<нд>".ToUpper(), "").ToLower();

                rez.mask = mask;
                rez.clear_mask = GetClearMask(mask, DateTime.Now);
                //DateTime ddt = DateTime.Now;


                return rez;
            }
            catch (Exception ex)
            {
                addlog.Debug("при розборі маски {0} виникла помилка: {1}",text,ex.Message);
                return rez;
            }
        }

        /// <summary>
        /// Get clear string without patterns
        /// </summary>
        /// <param name="mask">patterns</param>
        /// <param name="ddt">DateTime for replace patterns</param>
        /// <returns></returns>
        private static string GetClearMask(string mask, DateTime ddt)
        {
            try
            {
                int m = mask.IndexOf("<");
                if (m >= 0)
                {
                    string tmp = mask[m + 1].ToString() + mask[m + 2];
                    if (tmp == "*>" || tmp == ".*" || tmp == "*.")
                        m = mask.IndexOf("<", m + 1);
                }

                if (m >= 0)
                {
                    mask = mask.Substring(0, mask.IndexOf("<", m)) + GetDateMask(mask.Substring(mask.IndexOf("<", m) + 1, mask.IndexOf(">", m) - mask.IndexOf("<", m) - 1), ddt) + mask.Substring(mask.IndexOf(">", m) + 1);
                    if (mask.IndexOf("<", m) >= 0)
                    {
                        mask = GetClearMask(mask, ddt);
                    }
                    else
                        return mask;
                }
                return mask;
            }
            catch (Exception ex)
            {
                addlog.Debug("при отриманні чистої маски {0} виникла помилка: {1}", mask, ex.Message);
                return "";
            }
        }

            /// <summary>
            /// Replace pattern by string 
            /// </summary>
            /// <param name="mask">pattern</param>
            /// <param name="ddt">DateTime for replace</param>
            /// <returns></returns>
        private static string GetDateMask(string mask, DateTime ddt)
        {
            try
            {
                if (mask.IndexOf("+") >= 0 || mask.IndexOf("-") >= 0)
                {
                    ddt = NewDate(mask, ddt, out mask);
                }

                mask = mask.Replace('h', 'H').Replace('m', 'M').Replace('n', 'm').Replace("dddddd", "D").Replace("ddddd", "d");
                return ddt.ToString(mask);
            }
            catch(Exception ex)
            {
                switch (mask)
                {
                    case "*":
                        return "<*>";
                    case ".*":
                        return "<.*>";
                    case "*.":
                        return "<*.>";
                    default:
                        return mask;
                }                
            }
        }

        private static DateTime NewDate(string mask, DateTime ddt, out string new_mask)
        {
            try
            {
                int d = 0;
                int j = 0;
                string znak = "+";
                while (j < mask.Length)
                {
                    if (mask[j] == '+')
                    {
                        znak = "+";
                        break;
                    }
                    if (mask[j] == '-')
                    {
                        znak = "-";
                        break;
                    }
                    j++;
                }

                if (mask.IndexOf(znak) >= 0)
                    if (char.IsDigit(mask[mask.IndexOf(znak) + 1]))
                    {
                        char oper = mask[mask.IndexOf(znak) - 1];
                        int i = mask.IndexOf(znak) + 1;
                        string tmp = znak;
                        while (char.IsDigit(mask, i))
                        {
                            tmp += mask[i];
                            i++;
                            if (i == mask.Length) break;
                        }
                        mask = mask.Substring(0, mask.IndexOf(znak)) + mask.Substring(i);
                        d = Int32.Parse(tmp);

                        switch (oper)
                        {
                            case 'd':
                                ddt = ddt.AddDays(d);
                                break;
                            case 'm':
                                ddt = ddt.AddMonths(d);
                                break;
                            case 'y':
                                ddt = ddt.AddYears(d);
                                break;
                            case 'h':
                                ddt = ddt.AddHours(d);
                                break;
                            case 'n':
                                ddt = ddt.AddMinutes(d);
                                break;
                            case 's':
                                ddt = ddt.AddSeconds(d);
                                break;
                        }

                        if (mask.IndexOf("+") >= 0 || mask.IndexOf("-") >= 0)
                        {
                            ddt = NewDate(mask, ddt, out mask);
                        }


                    }
                new_mask = mask;
                return ddt;
            }
            catch (Exception ex)
            {
                addlog.Error("При отриманні маски {0} з дати {1} виникла помилка: {2}", mask, ddt.ToString(), ex.Message);
                new_mask = "";
                return ddt;                
            }
        }

        private static Stream ZipingFile(IEnumerable<string> files)
        {
            try
            {
                Stream rez = new MemoryStream();
                ZipFile zip = new ZipFile(Encoding.UTF8)
                {
                    CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression,
                    CompressionMethod = CompressionMethod.BZip2
                };
               // zip.UpdateFile()
                zip.SaveProgress += SaveProgress;
                foreach (string f in files)
                {
                    zip.UpdateFile(f, "");
                    addlog.Debug("В архів додано файл - {0}", f);
                }
                //zip.AddFiles(files);
                zip.Save(rez);

                return rez;
            }
            catch (Exception ex)
            {
                addlog.Error("При архівуванні файлів виникла помилка: {0}", ex.Message);
                return null;
            }


        }

        private static void SaveProgress(object sender, SaveProgressEventArgs e)
        {
            switch (e.EventType)
            {
                case ZipProgressEventType.Saving_Started:
                    addlog.Debug("Початок архівування файлів");
                    break;
               /* case ZipProgressEventType.Saving_BeforeWriteEntry:
                    addlog.Debug("{0}, було - {1}, стало - {2}", e.CurrentEntry.FileName, e.CurrentEntry.UncompressedSize, e.CurrentEntry.CompressedSize);
                    break;*/
                case ZipProgressEventType.Saving_Completed:
                    addlog.Debug("Кінець архівування");
                    break;
            }
        }

        private static void CopyFileTCP(string IP, string destination, IEnumerable<string> files, bool owerwrite)
        {
            try
            {
                if (SendFileTCP(IP, destination, files, owerwrite))
                {    
                    addlog.Info("Файли успішно скопійовано");
                }
            }
            catch (Exception ex)
            {
                addlog.Error("Помилка при копіювані файлів по ТСР: {0}", ex.Message);
            }
        }

        private static void MoveFileTCP(string IP, string destination, IEnumerable<string> files, bool owerwrite)
        {
            try
            {
                if (SendFileTCP(IP, destination, files, owerwrite))
                {
                    foreach (string f in files)
                        File.Delete(f);

                    addlog.Info("Файли успішно переміщено");
                }
            }
            catch(Exception ex)
            {
                addlog.Error("Помилка при переміщенні файлів по ТСР: {0}", ex.Message);
            }
        }

        private static bool SendFileTCP(string IP, string destination, IEnumerable<string> files, bool owerwrite)
        {
            try
            {
               // if (files.Count() == 0)
               //     return false;

                addlog.Debug("Початок відправки файлів по ТСР на {0}", IP);

                Stream fs = ZipingFile(files);
                if (fs == null)
                    return false;

                fs.Seek(0, SeekOrigin.Begin);

                TcpClient tcpC = new TcpClient(IP, 30001);
               // tcpC.SendTimeout=1000;
                NetworkStream nts = tcpC.GetStream();
                BinaryFormatter format = new BinaryFormatter();
                byte[] buf = new byte[1280];
                int count;
                BinaryReader br = new BinaryReader(fs);
                long k = fs.Length;
                format.Serialize(nts, k.ToString() + "|" + destination + "|" + Convert.ToInt32(owerwrite).ToString()+"|"+UpdateApl.GetChecksumm(fs)); //Send size and destination and Check sum

                fs.Seek(0, SeekOrigin.Begin);
                addlog.Debug("байти пішли");
                while ((count = br.Read(buf, 0, 1280)) > 0)
                {
                    format.Serialize(nts, buf);
                    //addlog.Debug("Передано {0} байт", count);
                }

              //  string ok = (string)(format.Deserialize(nts));

               // addlog.Info(ok);

                fs.Close();

                addlog.Info("Файли успішно відправлено");
                return true;
            }
            catch(Exception ex)
            {
                addlog.Error("При відправці файлів по ТСР виникла помилка: {0}", ex.Message);
                return false;
            }            

        }
        
    }
}
