using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Witch.GUI.HTML
{
    public class HTMLAttributeExtractor
    {
        private const string UNIQUE_ID = "id";
        private const char PARAMETER_SEPARATOR = '=';

        public string GetUniqueId(Dictionary<string, string> attributes)
        {
            if (attributes.ContainsKey(UNIQUE_ID))
            {
                string value = null;
                attributes.TryGetValue(UNIQUE_ID, out value);
                return value.Replace("\"", ""); ;
            }
            return "";
        }

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