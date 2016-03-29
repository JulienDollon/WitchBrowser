using System;
using System.Collections.Generic;

namespace Witch.GUI.HTML
{
    class HTMLTreeBuilder
    {
        private HTMLControlFactory htmlControlfactory = new HTMLControlFactory();
        private string[] splitTags(string htmlSource)
        {
            char[] charSeparators = new char[] { '>' };
            string[] arrayOfTag = htmlSource.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
            return arrayOfTag;
        }

        public NTree<IHTMLControl> BuildTree(string htmlSource)
        {
            string[] arrayOfTag = splitTags(htmlSource);

            NTree<IHTMLControl> tree = null;
            NTree<IHTMLControl> currentPointer = null;

            for (int i = 0; i < arrayOfTag.Length; i++)
            {
                IHTMLControl element = htmlControlfactory.CreateControl(arrayOfTag[i]);
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
                    htmlControlfactory.MergeControl(currentPointer.Data, element);
                    currentPointer = currentPointer.Parent;
                }
                else
                {
                    throw new KeyNotFoundException("Not supposed to come here, ever");
                }
            }
            return tree;
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