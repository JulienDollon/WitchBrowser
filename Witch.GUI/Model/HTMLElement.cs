using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Witch.GUI.Model
{
    class HTMLElement
    {
        public HTMLElement(HTMLTag tag)
        {
            this.Tag = tag;
        }

        public HTMLTag Tag { get; }

        public string InnerText { get; set; }
    }
}