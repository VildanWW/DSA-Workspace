using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures_CSharp.Sorting {
    internal static class Selection {
        public static void SelectionSort<T>(IList<T> container, Func<T, T, bool> func) {
            for (int i = 0; i < container.Count; i++) {
                int minIndex = i;

                for (int j = i + 1; j < container.Count; j++) {
                    if(func(container[j], container[minIndex])) {
                        minIndex = j;
                    }
                }

                if(minIndex != i) {
                    (container[i], container[minIndex]) = (container[minIndex], container[i]);
                }
            }
        }
    }
}
