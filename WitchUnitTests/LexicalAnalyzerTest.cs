using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witch.GUI.JavaScript.LexicalAnalysis;

namespace WitchUnitTests
{
    [TestClass]
    public class LexicalAnalyzerTest
    {
        private readonly LexicalAnalyzer analyzer = new LexicalAnalyzer();
        [TestMethod]
        public void Tokenize_withSimpleAddition()
        {
            var testToExecute = "var i = 1 + 2 ;";
            var tokens = analyzer.Tokenize(testToExecute);
            Assert.AreEqual(tokens.Count, 7);
            Assert.AreEqual(tokens[0].Type, TokenType.Identifier);
            Assert.AreEqual(tokens[0].Value, "var");
            Assert.AreEqual(tokens[1].Type, TokenType.Name);
            Assert.AreEqual(tokens[1].Value, "i");
            Assert.AreEqual(tokens[2].Type, TokenType.Equal);
            Assert.AreEqual(tokens[2].Value, "=");
            Assert.AreEqual(tokens[3].Type, TokenType.Digit);
            Assert.AreEqual(tokens[3].Value, "1");
            Assert.AreEqual(tokens[4].Type, TokenType.Operator);
            Assert.AreEqual(tokens[4].Value, "+");
            Assert.AreEqual(tokens[6].Type, TokenType.Identifier);
            Assert.AreEqual(tokens[6].Value, ";");
        }
    }
}