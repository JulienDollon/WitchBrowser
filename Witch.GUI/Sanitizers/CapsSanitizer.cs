using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Witch.GUI.Model;

namespace Witch.GUI.Sanitizers
{
    class CapsSanitizer : ISanitizer
    {
        public string Sanitize(string sanitizeFrom)
        {
            foreach (HTMLTag tag in HTMLTag.AllTags)
            {
                Regex r = new Regex("<" + tag.Value + ">", RegexOptions.IgnoreCase);
                sanitizeFrom = r.Replace(sanitizeFrom, "<" + tag.Value + ">"); 
                r = new Regex("</" + tag.Value + ">", RegexOptions.IgnoreCase);
                sanitizeFrom = r.Replace(sanitizeFrom, "</" + tag.Value + ">");
            }
            return sanitizeFrom;
        }
    }
}