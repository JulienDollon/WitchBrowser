using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witch.GUI.JavaScript.LexicalAnalysis;

namespace Witch.GUI.JavaScript.SyntacticalAnalyzer
{
    public class Statement
    {
        public Statement()
        {
            this.Tokens = new List<Token>();
        }

        public List<Token> Tokens { get; set; }
    }
}