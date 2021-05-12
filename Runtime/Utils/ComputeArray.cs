using System.Collections;
using System.Collections.Generic;

namespace ReGizmo.Utils
{
    public class ComputeArray<T>
        where T : unmanaged
    {
        T[] data;
        int cursor;

        public int Count => cursor;

        public ComputeArray(int capacity = 32)
        {
            data = new T[capacity];
            cursor = 0;
        }

        public ref T this[int i]
        {
            get
            {
                return ref this.data[i];
            }
        }

        public IEnumerable<T> GetEnumerable()
        {
            return this.data;
        }

        /// <summary>
        /// Adds a value to the array
        /// </summary>
        /// <param name="value">Value to add</param>
        public void Add(in T value)
        {
            data[cursor++] = value;
            if (cursor >= data.Length)
            {
                Resize(32);
            }
        }

        /// <summary>
        /// Soft-clears the array
        /// </summary>
        public void Clear()
        {
            cursor = 0;
        }

        /// <summary>
        /// Copies used data into given buffer
        /// </summary>
        /// <exception cref="System.ArgumentException">If given array cant fit the data</exception>
        /// <param name="dest">Array to copy data into</param>
        /// <param name="offset">Offset into destination array</param>
        public int Copy(T[] dest, int offset)
        {
            if ((dest.Length - offset) < cursor)
            {
                throw new System.ArgumentException("Given destination array is too small");
            }

            System.Array.Copy(data, 0, dest, offset, cursor);
            return cursor;
        }

        void Resize(int by)
        {
            var dest = new T[cursor + by];
            System.Array.Copy(data, 0, dest, 0, cursor);
            data = dest;
        }
    }
}