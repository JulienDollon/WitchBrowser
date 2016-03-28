using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witch.GUI.Model;

namespace Witch.GUI.Serializers
{
    class HTMLTreeSerializer
    {
        private HTMLElement extractTag(string line)
        {
            if (string.IsNullOrWhiteSpace(line) || !line.Contains("<"))
            {
                return null;
            }

            string[] splittedLine = line.Split('<');
            string stringTag = splittedLine[1];
            HTMLTag serializedTag = HTMLTag.Lookup(stringTag);
            var htmlElt = new HTMLElement(serializedTag);
            if (serializedTag.ClosingTag)
            {
                htmlElt.InnerText = splittedLine[0];
            }
            return htmlElt;
        }

        public NTree<HTMLElement> serializeTree(string content)
        {
            var arrayOfTag = content.Split('>');

            NTree<HTMLElement> tree = null;
            NTree<HTMLElement> currentPointer = null;

            for (int i = 0; i < arrayOfTag.Length; i++)
            {
                HTMLElement element = extractTag(arrayOfTag[i]);
                if (element == null)
                {
                    continue;
                }
                else if (IsFirstHtmlTag(element))
                {
                    tree = new NTree<HTMLElement>(element);
                    currentPointer = tree;
                }
                else if (!IsClosingHtmlTag(element))
                {
                    var newHtmlElementInTree = currentPointer.AddChild(element, currentPointer);
                    currentPointer = newHtmlElementInTree;
                }
                else
                {
                    currentPointer.Data.InnerText = element.InnerText;
                    currentPointer = currentPointer.Parent;
                }
            }
            return tree;
        }

        private bool IsClosingHtmlTag(HTMLElement element)
        {
            return element.Tag.ClosingTag;
        }

        private bool IsFirstHtmlTag(HTMLElement element)
        {
            return element.Tag.Value == HTMLTag.HTML.Value && !element.Tag.ClosingTag;
        }
    }
}