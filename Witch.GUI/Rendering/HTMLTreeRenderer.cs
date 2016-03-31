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
    public class HTMLTreeRenderer
    {
        public HTMLTreeRenderer(ItemsControl panel)
        {
            if (panel == null)
            {
                throw new ArgumentNullException();
            }
            this.panel = panel;
        }

        private readonly ItemsControl panel;

        public void Render(HTMLTree tree)
        {
            if (tree == null)
            {
                throw new ArgumentException();
            }

            panel.Items.Clear();
            NTree<IHTMLControl>.DFS(tree.Root, (NTree<IHTMLControl> control) =>
            {
                renderElement(control.Data);
            });
        }

        private HTMLControlUITypeRetriever rendererRetriever = new HTMLControlUITypeRetriever();
        private void renderElement(IHTMLControl control)
        {
            HTMLControlUI renderer = this.rendererRetriever.Retrieve(control.GetType());
            if (renderer == null)
            {
                return;
            }

            UIElement element = renderer.Generate(control);
            panel.Items.Add(element);
        }
    }
}