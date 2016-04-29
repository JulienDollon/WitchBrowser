using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witch.GUI.JavaScript.LexicalAnalysis;
using Witch.GUI.JavaScript.SyntacticalAnalyzer;

namespace WitchUnitTests
{
    [TestClass]
    public class StatementParserTest
    {
        private readonly StatementParser parser = new StatementParser();
        private readonly LexicalAnalyzer analyzer = new LexicalAnalyzer();
        [TestMethod]
        public void Parse_withSimpleStatement()
        {
            var testToExecute = "var i = 1 + 2 ;";
            var tokens = analyzer.Tokenize(testToExecute);
            var statements = parser.Parse(tokens);
            Assert.AreEqual(statements.Count, 1);
        }

        [TestMethod]
        public void Parse_withMultipleStatement()
        {
            var testToExecute = "var i = 1 + 2 ; var i = 1 + 2 ; var i = 1 + 2 ;";
            var tokens = analyzer.Tokenize(testToExecute);
            var statements = parser.Parse(tokens);
            Assert.AreEqual(statements.Count, 3);
        }

        [TestMethod]
        public void Parse_withIfStatement()
        {
            var testToExecute = "var i = 1 + 2 ; i = i + i ; if (i == 5) { alert('lol') ; } else { alert('i') ; }";
            var tokens = analyzer.Tokenize(testToExecute);
            var statements = parser.Parse(tokens);
            Assert.AreEqual(statements.Count, 4);
        }
    }
}