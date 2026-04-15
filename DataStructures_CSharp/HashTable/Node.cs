
using System.ComponentModel.Design;

namespace DataStructures_CSharp.HashTable {
    public class Node<KeyType, ValueType> {
        public KeyType Key { get; set; }
        public ValueType Value { get; set; }
        public Node<KeyType, ValueType> Next { get; set; }

        public Node(KeyType key, ValueType value) {
            Key = key;
            Value = value;
        }
    }
}
