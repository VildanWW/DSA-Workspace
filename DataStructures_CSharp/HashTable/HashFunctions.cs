
using System.Runtime.InteropServices;

namespace DataStructures_CSharp.HashTable {
    public static class HashFunctions {
        private static byte[] GetRawBytes(object obj) {
            if (obj == null) return Array.Empty<byte>();
            if (obj is string s) return System.Text.Encoding.UTF8.GetBytes(s);


            try {
                int size = Marshal.SizeOf(obj);
                byte[] bytes = new byte[size];
                IntPtr ptr = Marshal.AllocHGlobal(size);
                try {
                    Marshal.StructureToPtr(obj, ptr, false);
                    Marshal.Copy(ptr, bytes, 0, size);
                }
                finally {
                    Marshal.FreeHGlobal(ptr);
                }
                return bytes;
            }
            catch {
                
                return BitConverter.GetBytes(obj.GetHashCode());
            }
        }

       
        public static int FnvHash(object key, int tableSize) {
            uint hash = 2166136261;
            uint prime = 16777619;
            byte[] bytes = GetRawBytes(key);

            foreach (byte b in bytes) {
                hash *= prime;
                hash ^= b;
            }
            return (int)(hash % (uint)tableSize);
        }

        
        public static int Fnv1aHash(object key, int tableSize) {
            uint hash = 2166136261;
            uint prime = 16777619;
            byte[] bytes = GetRawBytes(key);

            foreach (byte b in bytes) {
                hash ^= b;     
                hash *= prime;
            }
            return (int)(hash % (uint)tableSize);
        }

        public static int MurmurHash3(object key, int tableSize) {
            byte[] bytes = GetRawBytes(key);
            uint h1 = 0x12345678;

            foreach (byte b in bytes) {
                uint k1 = b;
                k1 *= 0xcc9e2d51;
                k1 = (k1 << 15) | (k1 >> 17);
                k1 *= 0x1b873593;

                h1 ^= k1;
                h1 = (h1 << 13) | (h1 >> 19);
                h1 = h1 * 5 + 0xe6546b64;
            }

            h1 ^= (uint)bytes.Length;
            h1 ^= h1 >> 16;
            h1 *= 0x85ebca6b;
            h1 ^= h1 >> 13;
            h1 *= 0xc2b2ae35;
            h1 ^= h1 >> 16;

            return (int)(h1 % (uint)tableSize);
        }
    }
}
