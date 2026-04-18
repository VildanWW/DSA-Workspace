using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures_CSharp.Sorting {
    public static class HeapSort {

        public static void SortIncrease<T>(this IList<T> container) where T : IComparable<T> {
            int n = container.Count;

            for (int i = n / 2 - 1; i >= 0; i--) {
                SiftDownIncrease(container, n, i);
            }

            GetResultIncrease(container);
        }


        private static void SiftDownIncrease<T>(IList<T> container, int n, int i) where T : IComparable<T> {
            while (true) {
                int largest = i;
                int left = 2 * i + 1;
                int right = 2 * i + 2;

                if (left < n && container[largest].CompareTo(container[left]) < 0) largest = left;
                if (right < n && container[largest].CompareTo(container[right]) < 0) largest = right;

                if (largest == i) break;

                (container[largest], container[i]) = (container[i], container[largest]);
                i = largest;
            }
        }

        private static void GetResultIncrease<T>(IList<T> container) where T : IComparable<T> {
            for (int i = container.Count - 1; i >= 0; i--) {
                (container[i], container[0]) = (container[0], container[i]);
                SiftDownIncrease(container, i, 0);
            }
        }

        public static void SortDecrease<T>(this IList<T> container) where T : IComparable<T> {
            int n = container.Count;
            for (int i = n / 2 - 1; i >= 0; i--) {
                SiftDownDecrease(container, n, i);
            }
            GetResultDecrease(container);
        }

        private static void SiftDownDecrease<T>(IList<T> container, int n, int i) where T : IComparable<T> {
            while (true) {
                int smallest = i;
                int left = 2 * i + 1;
                int right = 2 * i + 2;

                if (left < n && container[smallest].CompareTo(container[left]) > 0) smallest = left;
                if (right < n && container[smallest].CompareTo(container[right]) > 0) smallest = right;

                if (smallest == i) break;

                (container[smallest], container[i]) = (container[i], container[smallest]);
                i = smallest;
            }
        }

        private static void GetResultDecrease<T>(IList<T> container) where T : IComparable<T> {
            for (int i = container.Count - 1; i >= 0; i--) {
                (container[i], container[0]) = (container[0], container[i]);
                SiftDownDecrease(container, i, 0);
            }
        }
    }
}