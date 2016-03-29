using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witch.GUI.HTMLModel;
using Witch.GUI.Model;

namespace Witch.GUI.Serializers
{
    class HTMLTreeSerializer
    {
        private IHTMLControl instantiateHtmlControl(string line)
        {
            if (string.IsNullOrWhiteSpace(line) || !line.Contains("<"))
            {
                throw new KeyNotFoundException();
            }

            string[] splittedLine = line.Split('<');
            string stringTag = splittedLine[1];
            IHTMLControl control = HTMLControlFactory.CreateControl(stringTag);
            if (hasInnerText(control))
            {
                setInnerText(control, splittedLine[0]);
            }
            return control;
        }

        private void setInnerText(IHTMLControl control, string innerText)
        {
            ((IInnerTextProperty)control).InnerText = innerText;
        }

        private string[] splitTags(string htmlSource)
        {
            char[] charSeparators = new char[] { '>' };
            string[] arrayOfTag = htmlSource.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
            return arrayOfTag;
        }

        public NTree<IHTMLControl> serializeTree(string htmlSource)
        {
            string[] arrayOfTag = splitTags(htmlSource);

            NTree<IHTMLControl> tree = null;
            NTree<IHTMLControl> currentPointer = null;

            for (int i = 0; i < arrayOfTag.Length; i++)
            {
                IHTMLControl element = instantiateHtmlControl(arrayOfTag[i]);
                if (isFirstHtmlTag(element))
                {
                    tree = new NTree<IHTMLControl>(element);
                    currentPointer = tree;
                }
                else if (!isClosingHtmlTag(element))
                {
                    var newHtmlElementInTree = currentPointer.AddChild(element, currentPointer);
                    currentPointer = newHtmlElementInTree;
                }
                else if (isClosingHtmlTag(element))
                {
                    if(hasInnerText(element))
                    {
                        setInnerText(currentPointer.Data, getInnerText(element));
                    }
                    currentPointer = currentPointer.Parent;
                }
                else
                {
                    throw new KeyNotFoundException("Not supposed to come here, ever");
                }
            }
            return tree;
        }

        private string getInnerText(IHTMLControl element)
        {
            return ((IInnerTextProperty)element).InnerText;
        }

        private bool hasInnerText(IHTMLControl element)
        {
            return element is IInnerTextProperty;
        }

        private bool isClosingHtmlTag(IHTMLControl element)
        {
            return element.IsClosing;
        }

        private bool isFirstHtmlTag(IHTMLControl element)
        {
            return element is HTMLElement && !element.IsClosing;
        }
    }
}