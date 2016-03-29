using System;
using System.Collections.Generic;
using Witch.GUI.HTML;

namespace Witch.GUI.HTML
{
    class HTMLControlFactory
    {

        private bool IsClosingHtml(string rawHtml)
        {
            return rawHtml.Contains("/");
        }

        private void extractClosingHtmlTagData(
            string rawHtml,
            out string tagName,
            out bool isClosing,
            out string innerText,
            out Dictionary<string, string> parameters)
        {
            parameters = new Dictionary<string, string>();
            isClosing = true;
            string[] rawHtmlSplitted = rawHtml.Split(new[] { "</" }, StringSplitOptions.None);
            innerText = rawHtmlSplitted[0];
            tagName = rawHtmlSplitted[1];
        }

        private void extractOpeningHtmlTagData(
            string rawHtml,
            out string tagName,
            out bool isClosing,
            out string innerText,
            out Dictionary<string, string> parameters)
        {
            parameters = new Dictionary<string, string>();
            innerText = null;
            isClosing = false;
            string[] rawHtmlSplitted = rawHtml.Split(new char[] { '<' }, StringSplitOptions.RemoveEmptyEntries);

            if (hasParameters(rawHtmlSplitted[0]))
            {
                tagName = rawHtmlSplitted[0].Split(new[] { " " }, StringSplitOptions.None)[0];
                parameters = extractParameters(rawHtml);
            }
            else
            {
                tagName = rawHtmlSplitted[0];
            }
        }

        private void extractHtmlTagData(
            string rawHtml,
            out string tagName,
            out bool isClosing,
            out string innerText,
            out Dictionary<string, string> parameters)
        {
            if (IsClosingHtml(rawHtml))
            {
                extractClosingHtmlTagData(rawHtml, out tagName, out isClosing, out innerText, out parameters);
            }
            else
            {
                extractOpeningHtmlTagData(rawHtml, out tagName, out isClosing, out innerText, out parameters);
            }
        }

        private bool hasParameters(string rawHtml)
        {
            return rawHtml.Contains(" ") && !string.IsNullOrWhiteSpace(rawHtml);
        }

        private Dictionary<string, string> extractParameters(string tagString)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (tagString == null || !hasParameters(tagString))
            {
                return result;
            }

            string[] parameterStrings = tagString.Split(' ');
            for (int i = 1; i < parameterStrings.Length; i++)
            {
                HTMLParameterExtractor extractor = new HTMLParameterExtractor();
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

            string tagString = null;
            string innerText = null;
            bool isClosing = false;
            Dictionary<string, string> parameters = null;

            extractHtmlTagData(rawHtmlTag, out tagString, out isClosing, out innerText, out parameters);
            return createInstance(tagString, isClosing, innerText, parameters);
        }

        private IHTMLControl createInstance(string tagString, bool isClosing, string innerText, Dictionary<string, string> parameters)
        {
            foreach (Type tag in SupportedHtmlControls.AllElementsType)
            {
                var obj = (IHTMLControl)Activator.CreateInstance(tag);
                if (obj.ToString().Equals(tagString))
                {
                    obj.IsClosing = isClosing;
                    setInnerText(obj, innerText);
                    obj.Parameters = parameters;
                    return obj;
                }
            }
            return new UnknownElement() { IsClosing = isClosing };
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