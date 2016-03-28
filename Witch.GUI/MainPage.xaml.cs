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
using Witch.GUI.Model;

namespace Witch.GUI
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            string content = await Test.HTMLMocks.getHelloWorldWellFormedHtmlDocument();
            displayHtmlTest(content);
        }

        private void buildHtml()
        {
            string content = null;
            txt_input_doc.Document.GetText(Windows.UI.Text.TextGetOptions.None, out content);
            content = new Sanitizers.Sanitizer().Sanitize(content);
            var tree = new Serializers.HTMLTreeSerializer().serializeTree(content);
            displayTree(tree);
        }

        private void displayTree(NTree<HTMLElement> tree)
        {
            txt_output_tree.PlaceholderText = "";
            NTree<HTMLElement>.DFSInOrder(tree, displayNode);
        }

        private void displayHtmlTest(string content)
        {
            txt_input_doc.Document.SetText(Windows.UI.Text.TextSetOptions.None, content);
        }

        private void displayNode(NTree<HTMLElement> node)
        {
            int depth = node.ComputeDepth();
            string increment = new String('=', depth);
            txt_output_tree.PlaceholderText += increment + node.Data.Tag.Value + node.Data.InnerText + "\n";
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