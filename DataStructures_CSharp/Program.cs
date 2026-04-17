
using DataStructures_CSharp.HashTable;

namespace DataStructures_CSharp {
    class Program {
        static void Main(string[] args) {
            HashTable<int, int> keyValuePairs = new HashTable<int, int>();

            keyValuePairs.Add(2, 5);
            keyValuePairs.Add(3, 6);
            keyValuePairs.Add(4, 7);
            keyValuePairs.Add(5, 8);
            keyValuePairs.Add(6, 9);
            keyValuePairs.Add(7, 10);
            keyValuePairs.Add(8, 11);
            keyValuePairs.Add(9, 12);
            keyValuePairs.Add(8, 111112);
            keyValuePairs.Add(18, 111112);
            keyValuePairs.Add(38, 111112);
            keyValuePairs.Add(338, 111112);
            keyValuePairs.Add(385, 111112);

            keyValuePairs.ShowTable();
        }
    }
}