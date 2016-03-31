using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Witch.GUI.HTML
{
    public class HTMLTree
    {
        public HTMLTree(NTree<IHTMLControl> elements)
        {
            if (elements == null)
            {
                throw new ArgumentNullException();
            }
            this.Root = elements;
            buildIdCache();
        }

        private readonly Dictionary<string, NTree<IHTMLControl>> idsCache = new Dictionary<string, NTree<IHTMLControl>>();

        private void buildIdCache()
        {
            NTree<IHTMLControl>.DFSInOrder(this.Root, (NTree<IHTMLControl> control) =>
            {
                string id = control.Data.UniqueId;
                if (!string.IsNullOrWhiteSpace(id) && !idsCache.ContainsKey(id))
                {
                    idsCache.Add(id, control);
                }
            });
        }

        public NTree<IHTMLControl> Root { get; }

        public NTree<IHTMLControl> GetByElementId(string id)
        {
            NTree<IHTMLControl> value = null;
            idsCache.TryGetValue(id, out value);
            return value;
        }
    }
}