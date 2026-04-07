using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures_CSharp.Trees {
    public enum NodeColor {
        Red,
        Black
    }

    public class NodeRBT<T> {
        public T Value { get; set; }
        public NodeRBT<T> Left { get; set; }
        public NodeRBT<T> Right { get; set; }
        public NodeRBT<T> Parent { get; set; }
        public NodeColor Color { get; set; }

        public NodeRBT(T value) {
            Value = value;
            Color = NodeColor.Red;
        }
    }
}
