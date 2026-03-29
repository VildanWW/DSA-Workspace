using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures_CSharp.Trees {
    public class NodeBST<T> {
        public T Value { get; set; }
        public NodeBST<T>? Left { get; set; }
        public NodeBST<T>? Right { get; set; }

        public NodeBST(T value) {
            Value = value;
        }
    }
}
