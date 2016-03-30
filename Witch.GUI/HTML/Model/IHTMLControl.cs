using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witch.GUI.HTML;

namespace Witch.GUI.HTML
{
    public interface IHTMLControl
    {
        string ToString();
        bool IsClosing { get; set; }
        Dictionary<string, string> Attributes { get; set; }
        string UniqueId { get; }
    }
}