using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Witch.GUI.JavaScript.LexicalAnalysis
{
    class LexicalGrammar
    {
        private string[] operators = new string[] { "*", "-", "+", "/" };
        public bool IsOperator(string content)
        {
            return content.Length > 0 && operators.Contains(content);
        }

        private string[] identifiers = new string[] { "var", ";", "if", "else", "{", "}", "(", ")", "=="};
        public bool IsIdentifier(string content)
        {
            return content.Length > 0 && identifiers.Contains(content);
        }

        public bool IsDigit(string content)
        {
            return content.Length > 0 && content.All(c => char.IsDigit(c));
        }

        public bool IsName(string content)
        {
            return content.Length > 0 && !IsDigit(content) && !IsEqual(content) && !IsOperator(content) && !IsIdentifier(content);
        }

        public bool IsEqual(string content)
        {
            return content.Equals("=", StringComparison.CurrentCulture);
        }
    }
}