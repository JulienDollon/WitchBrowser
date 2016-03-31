using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Witch.GUI.HTML;

namespace Witch.GUI.Rendering
{
    public interface HTMLControlUI
    {
        UIElement Generate(IHTMLControl control);
    }
}