using System;
using System.Collections.Generic;
using Witch.GUI.HTML;

namespace Witch.GUI.HTML
{
    public class HTMLControlFactory
    {
        class Tag
        {
            public string TagName { get; set; }
            public bool IsClosing { get; set; }
            public string InnerText { get; set; }
            public Dictionary<string, string> Attributes { get; set; }
        }

        private bool isClosingHtml(string rawHtml)
        {
            return rawHtml.Contains("/") && !rawHtml.Contains("http");
        }

        private void extractClosingHtmlTagData(string rawHtml, out Tag tag)
        {
            tag = new Tag();
            tag.Attributes = new Dictionary<string, string>();
            tag.IsClosing = true;

            string[] rawHtmlSplitted = rawHtml.Split(new[] { "</" }, StringSplitOptions.None);
            tag.InnerText = rawHtmlSplitted[0];
            tag.TagName = rawHtmlSplitted[1];
        }

        private void extractOpeningHtmlTagData(string rawHtml, out Tag tag)
        {
            tag = new Tag();
            tag.Attributes = new Dictionary<string, string>();
            tag.InnerText = null;
            tag.IsClosing = false;
            string[] rawHtmlSplitted = rawHtml.Split(new char[] { '<' }, StringSplitOptions.RemoveEmptyEntries);

            if (hasAttributes(rawHtmlSplitted[0]))
            {
                tag.TagName = rawHtmlSplitted[0].Split(new[] { " " }, StringSplitOptions.None)[0];
                tag.Attributes = extractAttributes(rawHtml);
            }
            else
            {
                tag.TagName = rawHtmlSplitted[0];
            }
        }

        private void extractHtmlTagData(string rawHtml, out Tag tag)
        {
            rawHtml = rawHtml.Replace(">", "");
            if (isClosingHtml(rawHtml))
            {
                extractClosingHtmlTagData(rawHtml, out tag);
            }
            else
            {
                extractOpeningHtmlTagData(rawHtml, out tag);
            }
        }

        private bool hasAttributes(string rawHtml)
        {
            return rawHtml.Contains(" ") && !string.IsNullOrWhiteSpace(rawHtml);
        }

        private Dictionary<string, string> extractAttributes(string tagString)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (tagString == null || !hasAttributes(tagString))
            {
                return result;
            }

            string[] parameterStrings = tagString.Split(' ');
            for (int i = 1; i < parameterStrings.Length; i++)
            {
                HTMLAttributeExtractor extractor = new HTMLAttributeExtractor();
                var parameter = extractor.Extract(parameterStrings[i]);
                result.Add(parameter.Key, parameter.Value);
            }
            return result;
        }

        public void MergeControl(IHTMLControl openingHtml, IHTMLControl closingHtml)
        {
            if (openingHtml == null || closingHtml == null)
            {
                throw new InvalidOperationException();
            }

            if (openingHtml is IInnerTextProperty && closingHtml is IInnerTextProperty)
            {
                ((IInnerTextProperty)openingHtml).InnerText = ((IInnerTextProperty)closingHtml).InnerText;
            }
        }

        public IHTMLControl CreateControl(string rawHtmlTag)
        {
            if (rawHtmlTag == null || string.IsNullOrWhiteSpace(rawHtmlTag) || !rawHtmlTag.Contains("<"))
            {
                throw new InvalidOperationException();
            }

            Tag tag = null;
            extractHtmlTagData(rawHtmlTag, out tag);
            return createInstance(tag);
        }

        private IHTMLControl createInstance(Tag tag)
        {
            foreach (Type tagType in SupportedHtmlControls.AllElementsType)
            {
                var obj = (IHTMLControl)Activator.CreateInstance(tagType);
                if (obj.ToString().Equals(tag.TagName))
                {
                    obj.IsClosing = tag.IsClosing;
                    setInnerText(obj, tag.InnerText);
                    obj.Attributes = tag.Attributes;
                    return obj;
                }
            }
            return new UnknownElement() { IsClosing = tag.IsClosing };
        }

        private void setInnerText(IHTMLControl control, string innerText)
        {
            if (control is IInnerTextProperty)
            {
                innerText = innerText == null ? "" : innerText;
                ((IInnerTextProperty)control).InnerText = innerText;
            }
        }
    }
}