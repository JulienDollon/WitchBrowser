using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Witch.GUI.HTML
{
    public class HTMLParameterExtractor
    {
        private const char PARAMETER_SEPARATOR = '=';

        public KeyValuePair<string, string> Extract(string tagString)
        {
            if (tagString == null || string.IsNullOrWhiteSpace(tagString) || !tagString.Contains("="))
            {
                throw new InvalidOperationException();
            }
            
            string[] keyValueString = tagString.Split(PARAMETER_SEPARATOR);
            KeyValuePair<string, string> parameter = new KeyValuePair<string, string>(keyValueString[0], keyValueString[1]);
            return parameter;
        }
    }
}