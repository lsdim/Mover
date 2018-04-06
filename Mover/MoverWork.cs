using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Mover
{
    class MoverWork
    {
        public enum Operation
        {
            Copy = 1,
            CopyRepl = 11,
            Move = 2,
            MoveRepl = 21,
            Delete = 3,
            Run = 4,
            ComandProm = 5,
            Message = 6,
            Rename = 7,
            DeleteExcept = 8
        }

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
                    if (day) return true;

                return false;
            }
        }

        private string dir_from;
        private string dir_destin;
        private string mask;
        private Operation oper;

        public MoverWork(string _dir_from, string _dir_destin, string _mask, Operation _oper)
        {
            dir_from = _dir_from;
            dir_destin = _dir_destin;
            mask = _mask;
            oper = _oper;
        }

        //Main function
        public void Run()
        {
            MaskRez elem = Mask(mask);
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
                        
            switch (oper)
            {
                case Operation.Copy:
                    CopyFile(dir_destin, files, false);
                    break;
                case Operation.CopyRepl:
                    CopyFile(dir_destin, files, true);
                    break;
                case Operation.Move:
                    MoveFile(dir_destin, files, false);
                    break;
                case Operation.MoveRepl:
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

            }

           
            
        }

        private void CopyFile(string dectination, IEnumerable<string> fiels, bool owerwrite)
        {
            try
            {

                Directory.CreateDirectory(dectination);
                foreach (string f in fiels)
                {
                    try
                    {
                        File.Copy(f, Path.Combine(dectination, Path.GetFileName(f)), owerwrite);
                    }
                    catch (Exception ex)
                    {
                        // error
                    }
                }
            }
            catch (Exception ex)
            {
                //error CreaTE dIRECTORY
            }
        }

        private void MoveFile(string dectination, IEnumerable<string> fiels, bool owerwrite)
        {
            try
            {

                Directory.CreateDirectory(dectination);
                foreach (string f in fiels)
                {
                    try
                    {
                        if (owerwrite)
                            if (File.Exists(Path.Combine(dectination, Path.GetFileName(f))))
                                File.Delete(Path.Combine(dectination, Path.GetFileName(f)));
                        File.Move(f, Path.Combine(dectination, Path.GetFileName(f)));
                    }
                    
                    catch (Exception ex)
                    {
                        // error
                    }
                }
            }
            catch (Exception ex)
            {
                //error CreaTE dIRECTORY
            }
        }

        private void DeleteFile(IEnumerable<string> fiels)
        {               
            foreach (string f in fiels)
             {
                    try
                    {                        
                        File.Delete(f);
                    }

                    catch (Exception ex)
                    {
                        // error
                    }
             }   
            
        }

        private void RunFile(string resource, IEnumerable<string> fiels)
        {
            foreach (string f in fiels)
            {
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo(resource);
                    startInfo.WorkingDirectory = Path.GetDirectoryName(startInfo.FileName);
                    Process.Start(startInfo);
                    break;
                }
                catch(Exception ex)
                {

                }
            }
        }

        private void RunCMD(string commands, IEnumerable<string> fiels)
        {
            foreach (string f in fiels)
            {
                try
                {  
                     Process process = new Process();
                     ProcessStartInfo startInfo = new ProcessStartInfo();
                     startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                     startInfo.FileName = "cmd.exe";
                     startInfo.Arguments = "/c " + commands;
                     process.StartInfo = startInfo;
                     process.Start();
                    break;
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void Mess(string messText, IEnumerable<string> fiels)
        {
            foreach (string f in fiels)
            {
                try
                {
                    System.Threading.Thread mess = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(ShowMess));
                    mess.Start(messText);
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void ShowMess(object messText)
        {
            System.Windows.Forms.MessageBox.Show((string)messText, "Mover", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        }

        //Create List of Files (search files by pattern)
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
        public static MaskRez Mask(string text)
        {
            MaskRez rez = new MaskRez();
            string mask = text;

            Regex regex = new Regex(@".*?(<\d{2}:\d{2}>).*?");
            MatchCollection mc = regex.Matches(text);
            switch (mc.Count)
            {
                case 0:
                    break;
                case 1:
                    TimeSpan.TryParse(mc[0].Groups[1].Value.Replace("<", "").Replace(">","").Trim(), out rez.ttm1);
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
                    foreach(Match m in mc)
                        mask = mask.Replace(m.Groups[1].Value, "");
                    break;
            }

            rez.TCP = (text.IndexOf("<TCP>") >= 0) ? true : false;
            mask = mask.Replace("<TCP>", "");

            rez.Recurcia = (text.IndexOf("<RECUR>") >= 0) ? true : false;
            mask = mask.Replace("<RECUR>", "");
            

            bool[] day = new bool[7];

            day[0] = (text.IndexOf("<пн>",StringComparison.InvariantCultureIgnoreCase) >= 0) ?  true : false;
            day[1] = (text.IndexOf("<вт>", StringComparison.InvariantCultureIgnoreCase) >= 0) ? true : false;
            day[2] = (text.IndexOf("<ср>", StringComparison.InvariantCultureIgnoreCase) >= 0) ? true : false;
            day[3] = (text.IndexOf("<чт>", StringComparison.InvariantCultureIgnoreCase) >= 0) ? true : false;
            day[4] = (text.IndexOf("<пт>", StringComparison.InvariantCultureIgnoreCase) >= 0) ? true : false;
            day[5] = (text.IndexOf("<сб>", StringComparison.InvariantCultureIgnoreCase) >= 0) ? true : false;
            day[6] = (text.IndexOf("<нд>", StringComparison.InvariantCultureIgnoreCase) >= 0) ? true : false;

            rez.days = day;

            mask = mask.ToUpper().Replace("<пн>".ToUpper(), "").Replace("<вт>".ToUpper(), "")
                .Replace("<ср>".ToUpper(), "").Replace("<чт>".ToUpper(), "").Replace("<пт>".ToUpper(), "")
                .Replace("<сб>".ToUpper(), "").Replace("<нд>".ToUpper(), "").ToLower();
            
            rez.mask = mask;
            rez.clear_mask = GetClearMask(mask, DateTime.Now);
                //DateTime ddt = DateTime.Now;
            

            return rez;
        }

        private static string GetClearMask(string mask, DateTime ddt)
        {
            if (mask.IndexOf("<") >= 0)
            {
                mask = mask.Substring(0, mask.IndexOf("<")) + GetDateMask(mask.Substring(mask.IndexOf("<") + 1, mask.IndexOf(">") - mask.IndexOf("<") - 1),ddt) + mask.Substring(mask.IndexOf(">") + 1);
                if (mask.IndexOf("<") >= 0)
                {
                    mask = GetClearMask(mask, ddt);
                }
                else
                    return mask;
            }
            
            return mask;

        }

        private static string GetDateMask(string mask, DateTime ddt)
        {
            
            if (mask.IndexOf("+")>=0 || mask.IndexOf("-")>=0)
            {
                ddt = NewDate(mask, ddt, out mask);
            }

            mask = mask.Replace('h', 'H').Replace('m', 'M').Replace('n', 'm').Replace("dddddd", "D").Replace("ddddd", "d");
            return ddt.ToString(mask);
        }

        private static DateTime NewDate(string mask, DateTime ddt, out string new_mask)
        {
            int d=0;
            int j = 0;
            string znak = "+";
            while (j<mask.Length)
            {
                if (mask[j]=='+')
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
            if (char.IsDigit(mask[mask.IndexOf(znak)+1]))
            {
                char oper = mask[mask.IndexOf(znak) - 1]; 
                int i = mask.IndexOf(znak) +1;
                string tmp = znak;
                while(char.IsDigit(mask,i))
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

        
    }
}
