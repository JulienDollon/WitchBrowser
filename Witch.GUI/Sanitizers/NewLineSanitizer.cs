using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Witch.GUI.Sanitizers
{
    class NewLineSanitizer : ISanitizer
    {
        public string Sanitize(string sanitizeFrom)
        {
            return sanitizeFrom.Replace("\n", "").Replace("\r", "");
        }
    }
}