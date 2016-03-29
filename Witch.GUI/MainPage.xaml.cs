using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
using Witch.GUI.HTML;

namespace Witch.GUI
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void page_Loaded(object sender, RoutedEventArgs e)
        {
            string content = await Test.HTMLMocks.GetHelloWorldWellFormedHtmlDocument();
            displayHtmlTest(content);
        }

        private void buildHtml()
        {
            string content = null;
            txt_input_doc.Document.GetText(Windows.UI.Text.TextGetOptions.None, out content);
            content = new Sanitizers.Sanitizer().Sanitize(content);
            var tree = new HTMLTreeBuilder().BuildTree(content);
            displayTree(tree);
        }

        private void displayTree(NTree<IHTMLControl> tree)
        {
            txt_output_tree.PlaceholderText = "";
            NTree<IHTMLControl>.DFSInOrder(tree, displayNode);
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

            foreach (var parameter in node.Data.Parameters)
            {
                dataToDisplay = String.Format("{0} [Param:{1}] ", dataToDisplay, parameter.ToString());
            }

           txt_output_tree.PlaceholderText += String.Format("{0} \n", dataToDisplay);
        }

        private void txt_input_doc_TextChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                buildHtml();
                lbl_compile.Visibility = Visibility.Collapsed;
            }
            catch
            {
                lbl_compile.Visibility = Visibility.Visible;
            }
        }
    }
}