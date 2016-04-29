using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witch.GUI.JavaScript.LexicalAnalysis;

namespace Witch.GUI.JavaScript.SyntacticalAnalyzer
{
    public class SyntacticTree
    {
        public NTree<Token> Root { get; private set; }
        public SyntacticTree(NTree<Token> astTree)
        {
            if (astTree == null)
            {
                throw new ArgumentNullException();
            }
            this.Root = astTree;
        }
    }
}