using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Witch.GUI.HTMLModel;
using Witch.GUI.Model;

namespace Witch.GUI.Sanitizers
{
    class CapsSanitizer : ISanitizer
    {
        public string Sanitize(string sanitizeFrom)
        {
            foreach (string tag in HTMLControlFactory.getAllSupportedElements())
            {
                Regex r = new Regex("<" + tag + ">", RegexOptions.IgnoreCase);
                sanitizeFrom = r.Replace(sanitizeFrom, "<" + tag + ">");
                r = new Regex("</" + tag + ">", RegexOptions.IgnoreCase);
                sanitizeFrom = r.Replace(sanitizeFrom, "</" + tag + ">");
            }
            return sanitizeFrom;
        }
    }
}