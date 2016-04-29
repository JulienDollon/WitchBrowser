using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Witch.GUI.HTML;
using Witch.GUI.JavaScript.LexicalAnalysis;
using Witch.GUI.JavaScript.SyntacticalAnalyzer;
using Witch.GUI.Rendering;

namespace Witch.GUI
{
    /*This file is pure garbage and here just for debug/test purposes*/
    /*Dont even waste to read this code, it will just damage your brain*/
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void page_Loaded(object sender, RoutedEventArgs e)
        {
            string content = await Test.HTMLMocks.GetHelloWorldWellFormedHtmlDocument();
            this.renderer = new HTMLTreeRenderer(this.view_output);
            displayHtmlTest(content);
        }

        private HTMLTree tree;
        private void buildHtml()
        {
            string content = null;
            txt_input_doc.Document.GetText(Windows.UI.Text.TextGetOptions.None, out content);
            content = new Sanitizers.Sanitizer().Sanitize(content);
            tree = new HTMLTreeBuilder().BuildTree(content);
            displayTree();
        }

        private void interpretJavaScript()
        {
            var scriptElement = tree.FindScriptElements()[0];
            List<Token> tokens = executeLexicalAnalysis(scriptElement);
            displayLexicalAnalysisResults(tokens);

            SyntaxicTree JSTree = executeSyntacticalAnalysis(tokens);
            displaySyntacticalAnalysisResults(JSTree);
        }

        private SyntaxicTree executeSyntacticalAnalysis(List<Token> tokens)
        {
            SyntacticalAnalyzer analyzer = new SyntacticalAnalyzer();
            return analyzer.Parse(tokens);
        }

        private void displayLexicalAnalysisResults(List<Token> tokens)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Lexical Analysis:");
            builder.Append(string.Format("Tokens found:{0}\n", tokens.Count));

            foreach (var token in tokens)
            {
                builder.Append(string.Format("[{0}:{1}] ", token.Type.ToString(), token.Value));
            }

            txt_js_output_tree.Document.SetText(Windows.UI.Text.TextSetOptions.None, builder.ToString());
        }

        private void displaySyntacticalAnalysisResults(SyntaxicTree jSTree)
        {
            NTree<Token>.DFS(jSTree.Root, displayJSNode);
        }

        private void displayJSNode(NTree<Token> node)
        {
            int depth = node.ComputeDepth();
            string increment = new String('=', depth);
            string dataToDisplay = null;
            txt_js_syntax_output_tree.Document.GetText(Windows.UI.Text.TextGetOptions.None, out dataToDisplay);
            dataToDisplay += String.Format("{0} [TokenType:{1};TokenValue:{2}]", increment, node.Data.Type.ToString(), node.Data.Value);
            txt_js_syntax_output_tree.Document.SetText(Windows.UI.Text.TextSetOptions.None, dataToDisplay);
        }

        private List<Token> executeLexicalAnalysis(ScriptElement scriptElement)
        {
            LexicalAnalyzer analyzer = new LexicalAnalyzer();
            return analyzer.Tokenize(scriptElement.InnerText);
        }

        private void displayTree()
        {
            txt_output_tree.PlaceholderText = "";
            NTree<IHTMLControl>.DFS(tree.Root, displayNode);
        }

        private void displayHtmlTest(string content)
        {
            txt_input_doc.Document.SetText(Windows.UI.Text.TextSetOptions.None, content);
        }

        private void displayNode(NTree<IHTMLControl> node)
        {
            int depth = node.ComputeDepth();
            string increment = new String('=', depth);
            string dataToDisplay = String.Format("{0} {1} [ID:{2}]", increment, node.Data.ToString(), node.Data.UniqueId);

            if (node.Data is IInnerTextProperty)
            {
                dataToDisplay = String.Format("{0} [InnerText:{1}] ", dataToDisplay, ((IInnerTextProperty)node.Data).InnerText);
            }

            foreach (var parameter in node.Data.Attributes)
            {
                dataToDisplay = String.Format("{0} [Param:{1}] ", dataToDisplay, parameter.ToString());
            }

           txt_output_tree.PlaceholderText += String.Format("{0} \n", dataToDisplay);
        }

        private HTMLTreeRenderer renderer;
        private void renderHtml()
        {
            this.renderer.Render(tree);
        }

        private void txt_input_doc_TextChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                buildHtml();
                renderHtml();
                lbl_compile.Visibility = Visibility.Collapsed;
            }
            catch
            {
                lbl_compile.Visibility = Visibility.Visible;
            }
            interpretJavaScript();
        }
    }
}