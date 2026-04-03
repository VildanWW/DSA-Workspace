using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures_CSharp.Trees {
    public class NodeAVL<T> {
        public NodeAVL<T>? Left { get; set; }
        public NodeAVL<T>? Right { get; set; }
        public T Value { get; set; }
        public int Height { get; set; }

        public NodeAVL(T value) {
            Value = value;
            Height = 1;
        }
    }
}
