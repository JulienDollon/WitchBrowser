using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witch.GUI.HTML;

namespace WitchUnitTests
{
    [TestClass]
    public class HTMLTreeBuilderUnitTests
    {
        private readonly HTMLTreeBuilder builder = new HTMLTreeBuilder();
        [TestMethod]
        public void CreateTree_WithValidInput()
        {
            HTMLTree tree = builder.BuildTree("<HTML><BODY>hello world</BODY></HTML>");
            Assert.AreEqual(tree.Root.Data.ToString(), new HTMLElement().ToString());
            Assert.AreEqual(tree.Root.Children.First.Value.Data.ToString(), new BodyElement().ToString());
        }

        [TestMethod]
        public void GetByElementId_WithValidInputAndIds()
        {
            HTMLTree tree = builder.BuildTree("<HTML><BODY id=\"hello\">hello world</BODY></HTML>");
            Assert.IsNotNull(tree.GetByElementId("hello"));
        }
    }
}