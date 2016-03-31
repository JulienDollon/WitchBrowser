using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witch.GUI.HTML;

namespace Witch.GUI.HTML
{
    class SupportedHtmlControls
    {
        public static readonly List<Type> AllElementsType = new List<Type>()
        {
            typeof(BodyElement),
            typeof(HTMLElement),
            typeof(HeadElement),
            typeof(UnknownElement),
            typeof(H1Element),
            typeof(DivElement),
            typeof(IMGElement)
        };

        private static List<string> supportedElementsCache = null;
        public static List<string> GetSupportedElements()
        {
            if (supportedElementsCache != null)
            {
                return supportedElementsCache;
            }

            supportedElementsCache = new List<string>();
            foreach (Type type in AllElementsType)
            {
                var obj = (IHTMLControl)Activator.CreateInstance(type);
                supportedElementsCache.Add(obj.ToString());
            }
            return supportedElementsCache;
        }
    }
}