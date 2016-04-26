using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Witch.GUI.JavaScript.LexicalAnalysis
{
    public class LexicalAnalyzer
    {
        public List<Token> Tokenize(string javascript)
        {
            if (string.IsNullOrWhiteSpace(javascript))
            {
                throw new InvalidOperationException();
            }

            string[] untokenized_strings = javascript.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            List<Token> tokens = new List<Token>();
            foreach (string untokenized_string in untokenized_strings)
            {
                tokens.Add(tokenizeString(untokenized_string));
            }

            return tokens;
        }

        private LexicalGrammar grammar = new LexicalGrammar();
        private Token tokenizeString(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new LexicalAnalyzerException();
            }

            if (grammar.IsIdentifier(content))
            {
                return new Token(content, TokenType.Identifier);
            }

            if (grammar.IsOperator(content))
            {
                return new Token(content, TokenType.Operator);
            }

            if (grammar.IsDigit(content))
            {
                return new Token(content, TokenType.Digit);
            }

            if (grammar.IsEqual(content))
            {
                return new Token(content, TokenType.Equal);
            }

            if (grammar.IsName(content))
            {
                return new Token(content, TokenType.Name);
            }

            throw new LexicalAnalyzerException();
        }
    }
}