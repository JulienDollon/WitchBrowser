using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witch.GUI.JavaScript.LexicalAnalysis;

namespace Witch.GUI.JavaScript.SyntacticalAnalyzer
{
    public class SyntacticalAnalyzer
    {
        public SyntaxicTree Parse(List<Token> tokens)
        {
            if (tokens == null)
            {
                throw new SyntacticalAnalyzerException();
            }

            NTree<Token> tree = generateAST(tokens);
            return new SyntaxicTree(tree);
        }

        private NTree<Token> initializeTree()
        {
            Token root = new Token(TokenType.Root);
            NTree<Token> tree = new NTree<Token>(root);
            return tree;
        }

        private StatementParser statementParser = new StatementParser();
        private NTree<Token> generateAST(List<Token> tokens)
        {
            NTree<Token> ast = initializeTree();
            List<Statement> statements = statementParser.Parse(tokens);
            NTree<Token> currentPointer = ast;
            foreach (Statement statement in statements)
            {
                addStatementNode(currentPointer, statement);
            }

            //closeTree with return
            return ast;
        }

        private void addStatementNode(NTree<Token> currentPointer, Statement statement)
        {
            Token rootStatementToken = new Token(TokenType.Statement);
            NTree<Token> rootStatement = currentPointer.AddChild(rootStatementToken, currentPointer);
            //currentPointer = rootStatement;
            foreach (Token token in statement.Tokens)
            {
                //if (Token.IsIf(token))
                //{

                //}
                rootStatement.AddChild(token, rootStatement);
                //ignore ; and var
                //todo here + test
            }
        }
    }
}