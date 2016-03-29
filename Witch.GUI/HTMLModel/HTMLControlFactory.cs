using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witch.GUI.Model;

namespace Witch.GUI.HTMLModel
{
    class HTMLControlFactory
    {
        private static readonly List<Type> allElementsType = new List<Type>()
        {
            typeof(BodyElement),
            typeof(HTMLElement),
            typeof(HeadElement),
            typeof(UnknownElement),
            typeof(H1Element),
            typeof(DivElement)
        };

        public static IEnumerable<string> getAllSupportedElements()
        {
            foreach (Type type in allElementsType)
            {
                var obj = (IHTMLControl)Activator.CreateInstance(type);
                yield return obj.ToString();
            }
        }

        private static void extractTagString(string value, out string tag, out bool isClosing)
        {
            value = value.ToUpperInvariant();
            if (value.Contains("/"))
            {
                isClosing = true;
                tag = value.Split('/')[1];
            }
            else
            {
                tag = value;
                isClosing = false;
            }
        }

        public static IHTMLControl CreateControl(string value)
        {
            if (value == null || string.IsNullOrWhiteSpace(value))
            {
                throw new KeyNotFoundException();
            }

            string tagString = null;
            bool isClosing = false;
            extractTagString(value, out tagString, out isClosing);
            return createInstance(tagString, isClosing);
        }

        private static IHTMLControl createInstance(string tagString, bool isClosing)
        {
            foreach (Type tag in allElementsType)
            {
                var obj = (IHTMLControl)Activator.CreateInstance(tag);
                if (obj.ToString().Equals(tagString))
                {
                    obj.IsClosing = isClosing;
                    return obj;
                }
            }
            return new UnknownElement() { IsClosing = isClosing };
        }
    }
}