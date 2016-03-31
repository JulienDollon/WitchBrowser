using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witch.GUI.HTML;

namespace Witch.GUI.HTML
{
    public class UnknownElement : IHTMLControl
    {
        private readonly HTMLAttributeExtractor attributeExtractor = new HTMLAttributeExtractor();
        public Dictionary<string, string> Attributes
        {
            get;
            set;
        }

        public bool IsClosing
        {
            get;
            set;
        }

        public string UniqueId
        {
            get
            {
                return attributeExtractor.GetUniqueId(this.Attributes);
            }
        }

        public override string ToString()
        {
            return "UNKNOWN";
        }
    }
}