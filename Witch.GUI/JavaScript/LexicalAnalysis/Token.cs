using System;

namespace Witch.GUI.JavaScript.LexicalAnalysis
{
    public class Token
    {
        public Token(string value, TokenType type)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidOperationException();
            }

            this.Value = value;
            this.Type = type;
        }

        public string Value { get; }
        public TokenType Type { get; }
    }
}