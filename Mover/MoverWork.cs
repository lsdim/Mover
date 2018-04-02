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

            rez.TCP = (text.IndexOf("<TCP>") > 0) ? true : false;

            mask = mask.Replace("<TCP>", "");

            bool[] day = new bool[7];

            day[0] = (text.IndexOf("<пн>",StringComparison.InvariantCultureIgnoreCase) > 0) ?  true : false;
            day[1] = (text.IndexOf("<вт>", StringComparison.InvariantCultureIgnoreCase) > 0) ? true : false;
            day[2] = (text.IndexOf("<ср>", StringComparison.InvariantCultureIgnoreCase) > 0) ? true : false;
            day[3] = (text.IndexOf("<чт>", StringComparison.InvariantCultureIgnoreCase) > 0) ? true : false;
            day[4] = (text.IndexOf("<пт>", StringComparison.InvariantCultureIgnoreCase) > 0) ? true : false;
            day[5] = (text.IndexOf("<сб>", StringComparison.InvariantCultureIgnoreCase) > 0) ? true : false;
            day[6] = (text.IndexOf("<нд>", StringComparison.InvariantCultureIgnoreCase) > 0) ? true : false;

            rez.days = day;

            mask = mask.ToUpper().Replace("<пн>".ToUpper(), "").Replace("<вт>".ToUpper(), "")
                .Replace("<ср>".ToUpper(), "").Replace("<чт>".ToUpper(), "").Replace("<пт>".ToUpper(), "")
                .Replace("<сб>".ToUpper(), "").Replace("<нд>".ToUpper(), "");
            
            rez.mask = mask;

            Regex regdate = new Regex(@".*?(<.*?>).*?");
            MatchCollection mcd = regdate.Matches(mask);
            if (mcd.Count>0)
            {
                DateTime ddt = DateTime.Now;
                foreach (Match m in mcd)
                {
                    string msk =GetDateMask(mc[0].Groups[1].Value.Replace("<", "").Replace(">", "").Trim(),ddt);                    
                }
            }
            else
            {
                rez.clear_mask = mask;
            }

            return rez;
        }

        private static string GetDateMask(string mask, DateTime ddt)
        {
            return mask;
        }
    }
}
