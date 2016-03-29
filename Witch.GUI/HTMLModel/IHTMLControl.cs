using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Witch.GUI.Model
{
    public abstract class IHTMLControl
    {
        public abstract override string ToString();
        public bool IsClosing { get; set; }
    }
}