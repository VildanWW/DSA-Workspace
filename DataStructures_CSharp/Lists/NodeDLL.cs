using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures_CSharp.Lists {
    public class NodeDLL<T> {
        public T Value { get; set; }
        public NodeDLL<T>? Next { get; set; }
        public NodeDLL<T>? Prev { get; set; }

        public NodeDLL(T value) {
            Value = value;
        }
    }
}
