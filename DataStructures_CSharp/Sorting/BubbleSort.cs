using DataStructures_CSharp.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures_CSharp.Sorting {
    public static class BubbleSort {
        public static void Sort<T>(IList<T> container, Func<T, T, bool> func) {
            int sizeContainer = container.Count;

            for (int i = 0; i < sizeContainer; i++) {
                bool flag = true;

                for (int j = 0; j < sizeContainer - 1 - i; j++) {
                    if (func(container[j], container[j+1])) {
                        flag = false;
                        (container[j], container[j + 1]) = (container[j + 1], container[j]);
                    }
                }

                if (flag) break;
            }
        }

        public static void SortSinglyListOptimized<T>(SinglyLinkedList<T> container, Func<T, T, bool> func) {
            int sizeContainer = container.Count;

            for(int i=0;i<sizeContainer;i++) {
                Node<T> current = container.Head;
                bool flag = true;
                while (current.Next != null) {
                    if(func(current.Value, current.Next.Value)) {
                        flag = false;
                        (current.Value, current.Next.Value) = (current.Next.Value, current.Value);
                    }
                    current = current.Next;
                }
                
                if (flag) break;
            }
        }
    }
}
