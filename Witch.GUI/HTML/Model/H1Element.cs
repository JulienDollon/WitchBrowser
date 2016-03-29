using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witch.GUI.HTML;

namespace Witch.GUI.HTML
{
    class H1Element : IHTMLControl, IInnerTextProperty
    {
        public string InnerText { get; set; }
        public override string ToString()
        {
            return "H1";
        }
    }
}