using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Mover
{
    class MoverWork
    {
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
