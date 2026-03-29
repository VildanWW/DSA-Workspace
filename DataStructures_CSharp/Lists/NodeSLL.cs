using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures_CSharp.Lists {
    public class NodeSLL<T> {
        public T Value { get; set; }
        public NodeSLL<T>? Next { get; set; }

        public NodeSLL(T value) {
            Value = value;
        }
    }
}
