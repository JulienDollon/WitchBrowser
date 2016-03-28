using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Witch.GUI.Model
{
    class HTMLTag
    {
        private HTMLTag(string value) { Value = value; }
        public string Value { get; }
        public bool ClosingTag { get; set; }
        public static readonly List<HTMLTag> AllTags = new List<HTMLTag>() { HTML, HEAD, H1, TITLE, BODY, DIV };

        public static HTMLTag Lookup(string value)
        {
            if (value == null)
            {
                throw new KeyNotFoundException();
            }

            value = value.ToUpper();
            bool closingTag = false;
            if (value.Contains("/"))
            {
                closingTag = true;
                value = value.Split('/')[1];
            }
            foreach (var tag in AllTags)
            {
                if (tag.Value.Equals(value))
                {
                    tag.ClosingTag = closingTag;
                    return tag;
                }
            }

            return OTHER;
        }

        public static HTMLTag HTML 
        {
            get
            {
                return new HTMLTag("HTML");
            }
        }

        public static HTMLTag HEAD
        {
            get
            {
                return new HTMLTag("HEAD");
            }
        }

        public static HTMLTag DIV
        {
            get
            {
                return new HTMLTag("DIV");
            }
        }

        public static HTMLTag TITLE
        {
            get
            {
                return new HTMLTag("TITLE");
            }
        }

        public static HTMLTag BODY
        {
            get
            {
                return new HTMLTag("BODY");
            }
        }

        public static HTMLTag H1
        {
            get
            {
                return new HTMLTag("H1");
            }
        }

        public static HTMLTag OTHER
        {
            get
            {
                return new HTMLTag("OTHER");
            }
        }
    }
}