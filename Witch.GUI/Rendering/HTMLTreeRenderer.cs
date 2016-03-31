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
        public HTMLTreeRenderer(Canvas canvas)
        {
            if (canvas == null)
            {
                throw new ArgumentNullException();
            }
            this.canvas = canvas;
        }

        private readonly Canvas canvas;

        public void Render(HTMLTree tree)
        {
            if (tree == null)
            {
                throw new ArgumentException();
            }

            canvas.Children.Clear();
            NTree<IHTMLControl>.DFSInOrder(tree.Root, (NTree<IHTMLControl> control) =>
            {
                renderElement(control.Data);
            });
        }

        private HTMLControlUITypeRetriever rendererRetriever = new HTMLControlUITypeRetriever();
        private void renderElement(IHTMLControl control, double top = 0, double left = 0, int index = 1)
        {
            HTMLControlUI renderer = this.rendererRetriever.Retrieve(control.GetType());
            if (renderer == null)
            {
                return;
            }

            UIElement element = renderer.Generate(control);
            Canvas.SetLeft(element, left);
            Canvas.SetTop(element, top);
            Canvas.SetZIndex(element, index);
            canvas.Children.Add(element);
        }
    }
}