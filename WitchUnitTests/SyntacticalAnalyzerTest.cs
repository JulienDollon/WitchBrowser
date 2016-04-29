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
    class SyntacticalAnalyzerTest
    {
        private readonly SyntacticalAnalyzer syntacticalAnalyzer = new SyntacticalAnalyzer();
        private readonly LexicalAnalyzer lexicalAnalyzer = new LexicalAnalyzer();
        [TestMethod]
        public void Empty()
        {
        }
    }
}