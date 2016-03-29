using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Witch.GUI.Test
{
    class HTMLMocks
    {
        private static readonly string folder = "Test";
        private static readonly string filename = "helloworld.html";

        public static async Task<string> GetHelloWorldWellFormedHtmlDocument()
        {
            var rootFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var testFolder = await rootFolder.GetFolderAsync(folder);
            var file = await testFolder.GetFileAsync(filename);
            var content = await Windows.Storage.FileIO.ReadTextAsync(file);
            return content;
        }
    }
}