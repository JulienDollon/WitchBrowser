using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witch.GUI.JavaScript.LexicalAnalysis;

namespace Witch.GUI.JavaScript.SyntacticalAnalyzer
{
    public class StatementParser
    {
        private string[] END_OF_STATEMENT = new string[] { ";", "}" };
        private bool isEndOfStatementIdentifier(Token token)
        {
            if (token.Type != TokenType.Identifier)
            {
                return false;
            }

            foreach (string endOfStatement in END_OF_STATEMENT)
            {
                if (token.Value.Equals(endOfStatement, StringComparison.CurrentCulture))
                {
                    return true;
                }
            }

            return false;
        }

        public List<Statement> Parse(List<Token> tokens)
        {
            if (tokens == null)
            {
                throw new InvalidOperationException();
            }
            return generateStatements(tokens);
        }

        private List<Statement> generateStatements(List<Token> tokens)
        {
            List<Statement> statements = new List<Statement>();
            Statement currentStatement = new Statement();
            foreach (Token token in tokens)
            {
                currentStatement.Tokens.Add(token);
                if (isEndOfStatementIdentifier(token))
                {
                    statements.Add(currentStatement);
                    currentStatement = new Statement();
                }
            }
            return statements;
        }
    }
}