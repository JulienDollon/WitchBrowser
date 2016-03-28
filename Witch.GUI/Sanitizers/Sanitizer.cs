using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Witch.GUI.Sanitizers
{
    class Sanitizer
    {
        private readonly List<ISanitizer> sanitizers;

        public Sanitizer()
        {
            sanitizers = new List<ISanitizer>();
            sanitizers.Add(new NewLineSanitizer());
            sanitizers.Add(new CapsSanitizer());
        }

        public string Sanitize(string sanitizeFrom)
        {
            string result = sanitizeFrom;
            foreach (var sanitizer in sanitizers)
            {
                result = sanitizer.Sanitize(result);
            }
            return result;
        }
    }
}