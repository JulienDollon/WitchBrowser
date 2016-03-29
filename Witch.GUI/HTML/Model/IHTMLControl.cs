using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witch.GUI.HTML;

namespace Witch.GUI.HTML
{
    public abstract class IHTMLControl
    {
        private const string UNIQUE_ID = "id";
        public abstract override string ToString();
        public bool IsClosing { get; set; }
        public Dictionary<string, string> Parameters { get; set; }

        public string UniqueId
        {
            get
            {
                if (Parameters.ContainsKey(UNIQUE_ID))
                {
                    string value = null;
                    Parameters.TryGetValue(UNIQUE_ID, out value);
                    return value;
                }
                return Guid.NewGuid().ToString();
            }
        }
    }
}