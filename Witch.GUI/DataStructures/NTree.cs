using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Witch.GUI
{
    class NTree<T>
    {
        public T Data { get; }
        public LinkedList<NTree<T>> Children { get; }

        public NTree<T> Parent { get; }

        public NTree(T data, NTree<T> parent = null)
        {
            this.Data = data;
            this.Children = new LinkedList<NTree<T>>();
            this.Parent = parent;
        }

        public NTree<T> AddChild(T data, NTree<T> parent = null)
        {
            var node = new NTree<T>(data, parent);
            Children.AddFirst(node);
            return node;
        }

        public int ComputeDepth()
        {
            int i = 0;
            NTree<T> pointer = Parent;
            while (pointer != null) 
            {
                i++;
                pointer = pointer.Parent;
            }
            return i;
        }

        public static void DFSInOrder(NTree<T> node, Action<NTree<T>> visit)
        {
            if (node == null)
            {
                return;
            }

            visit(node);

            foreach (var item in node.Children)
            {
                DFSInOrder(item, visit);
            }
        }
    }
}