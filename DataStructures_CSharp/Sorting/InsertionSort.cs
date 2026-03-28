using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures_CSharp.Sorting {
    internal static class InsertionSort {
        public static void Sort<T>(IList<T> container, Func<T, T, bool> func) {
            for(int i=1;i<container.Count; i++) {
                T item = container[i];
                int j = i - 1;

                while(j>=0 && func(container[j], item)) {
                    container[j+1] = container[j];
                    j--;
                }

                container[j+1] = item;
            }
        }
    }
}
