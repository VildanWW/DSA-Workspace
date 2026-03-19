using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures_CSharp.Lists {
    internal class Node<T> {
        public T value { get; set; }
        public Node<T> next { get; set; }

        public Node(T value) {
            this.value = value;
        }
    }
}
