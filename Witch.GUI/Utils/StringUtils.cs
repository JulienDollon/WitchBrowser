using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Witch.GUI
{
    public static class StringUtils
    {
        public static string[] SplitAndKeep(this string s, string delims)
        {
            return Regex.Split(s, @"(?<=[" + delims + "])");
        }
    }
}
