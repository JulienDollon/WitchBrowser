using System;
using System.Collections.Generic;

namespace Witch.GUI.JavaScript.LexicalAnalysis
{
    public class Token
    {
        public Token(TokenType type, string value = null)
        {
            this.Value = value;
            this.Type = type;
            this.Annotations = new List<string>();
        }

        public string Value { get; }
        public TokenType Type { get; }
        public List<string> Annotations { get; set; }

        private static string IfIdentifier = "if";
        public static bool IsIf(Token token)
        {
            return token.Type == TokenType.Operator && token.Value.Equals(IfIdentifier, StringComparison.CurrentCulture);
        }
    }
}