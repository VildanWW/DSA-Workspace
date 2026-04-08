
namespace DataStructures_CSharp.Searching {
    public static class BinarySearching {
        private static int Find<T>(IList<T> container, int left, int right, T value) where T : IComparable<T> { 
            if(left==right) {
                if (Equals(container[left], value)) return left;
                return -1;
            }

            int sred = (left + right) / 2;
            int cmp = value.CompareTo(container[sred]);

            if (cmp > 0) return Find(container, sred + 1, right, value);
            else if (cmp < 0) return Find(container, left, sred - 1, value);

            return sred;
        }
        
        public static int Find<T>(IList<T> container, T value) where T : IComparable<T> {
            if (container == null || container.Count == 0) return -1;
            return Find(container, 0, container.Count - 1, value);
        }
    }
}
