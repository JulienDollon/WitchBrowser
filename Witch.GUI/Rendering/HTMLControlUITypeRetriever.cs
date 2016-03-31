using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witch.GUI.HTML;

namespace Witch.GUI.Rendering
{
    public class HTMLControlUITypeRetriever
    {
        public HTMLControlUITypeRetriever()
        {
            initializeRenderers();
        }

        private Dictionary<Type, Type> renderersTypes;
        private void initializeRenderers()
        {
            renderersTypes = new Dictionary<Type, Type>();
            renderersTypes.Add(typeof(H1Element), typeof(H1ElementUI));
            renderersTypes.Add(typeof(IMGElement), typeof(IMGElementUI));
        }

        public HTMLControlUI Retrieve(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException();
            }

            Type rendererType = null;
            renderersTypes.TryGetValue(type, out rendererType);

            if (rendererType == null)
            {
                return null;
            }

            var iuiElementRenderer = (HTMLControlUI)Activator.CreateInstance(rendererType);
            return iuiElementRenderer;
        }
    }
}