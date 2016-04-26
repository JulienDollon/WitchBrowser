using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Witch.GUI.HTML;
using Witch.GUI.JavaScript.LexicalAnalysis;
using Witch.GUI.Rendering;

namespace Witch.GUI
{
    /*This file is pure garbage and here just for debug/test purposes*/
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

            txt_js_output_tree.PlaceholderText = builder.ToString();
        }

        private List<Token> executeLexicalAnalysis(ScriptElement scriptElement)
        {
            JavaScript.LexicalAnalysis.LexicalAnalyzer analyzer = new JavaScript.LexicalAnalysis.LexicalAnalyzer();
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
                interpretJavaScript();
                lbl_compile.Visibility = Visibility.Collapsed;
            }
            catch
            {
                lbl_compile.Visibility = Visibility.Visible;
            }
        }
    }
}