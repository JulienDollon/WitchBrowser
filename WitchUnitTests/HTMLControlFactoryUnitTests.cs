using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Witch.GUI.HTML;

namespace WitchUnitTests
{
    [TestClass]
    public class HTMLControlFactoryUnitTests
    {
        private readonly HTMLControlFactory factory = new HTMLControlFactory();
        [TestMethod]
        public void CreateControl_OpenHtmlElement_WithValidInput()
        {
            IHTMLControl control = factory.CreateControl("<HTML>");
            Assert.AreEqual(control.IsClosing, false);
            Assert.AreEqual(control.ToString(), new HTMLElement().ToString());
        }

        [TestMethod]
        public void CreateControl_OpenHtmlElement_WithValidInputAndId()
        {
            IHTMLControl control = factory.CreateControl("<HTML id=\"hello\">");
            Assert.AreEqual(control.Attributes.Count, 1);
            Assert.AreEqual(control.UniqueId, "hello");
        }

        [TestMethod]
        public void CreateControl_CloseH1Element_WithValidInputAndInnerText()
        {
            IHTMLControl control = factory.CreateControl("yo</H1>");
            Assert.AreEqual(control is IInnerTextProperty, true);
            Assert.AreEqual((control as IInnerTextProperty).InnerText, "yo");
        }

        [TestMethod]
        public void MergeControl_H1Element_WithValidInput()
        {
            IHTMLControl openingControl = factory.CreateControl("<H1 id=\"hello\">");
            IHTMLControl closingControl = factory.CreateControl("inner text</H1>");
            factory.MergeControl(openingControl, closingControl);
            Assert.AreEqual((openingControl as IInnerTextProperty).InnerText, "inner text");
        }
    }
}