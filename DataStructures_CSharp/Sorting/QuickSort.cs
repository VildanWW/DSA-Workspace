using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures_CSharp.Sorting {
    public static class QuickSort {
        private static void Sort<T>(IList<T> container, Func<T, T, bool> func, int left, int right) {
            if (left > right) return;
            int sred = (left+ right) / 2;

            int l = left;
            int r = right;

            while(l<=r) {
                while(func(container[l], container[sred])) l++;
                while (func(container[sred], container[r])) r--;

                if(l<=r) {
                    (container[l], container[r]) = (container[r], container[l]);
                    l++;
                    r--;
                }
            }

            Sort(container, func, left, r);
            Sort(container, func, l, right);
        }

        public static void Sort<T>(IList<T> container, Func<T, T, bool> func) {
            Sort(container, func, 0, container.Count - 1);
        }
    }
}
