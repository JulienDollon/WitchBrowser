using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Witch.GUI.HTML;

namespace Witch.GUI.Rendering
{
    class H1ElementUI : HTMLControlUI
    {
        public UIElement Generate(IHTMLControl control)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = ((IInnerTextProperty)control).InnerText;
            return textBlock;
        }
    }
}