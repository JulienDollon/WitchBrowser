using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Witch.GUI.HTML;

namespace Witch.GUI.Rendering
{
    class IMGElementUI : HTMLControlUI
    {
        public UIElement Generate(IHTMLControl control)
        {
            string imageUrl = ((ISourceProperty)control).Source;
            Image image = new Image();
            BitmapImage bitmapImage = new BitmapImage();
            Uri uri = new Uri(imageUrl);
            bitmapImage.UriSource = uri;
            image.Source = bitmapImage;
            return image;
        }
    }
}