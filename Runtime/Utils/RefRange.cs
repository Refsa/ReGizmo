

namespace ReGizmo.Utils
{
    public ref struct RefRange<T>
        where T : unmanaged
    {
        T[] array;
        int start;
        int end;
        int current;

        public bool HasNext()
        {
            return current < end;
        }

        public ref T Next()
        {
            return ref array[current++];
        }

        public RefRange(T[] array, int start, int end)
        {
            this.start = start;
            this.end = end;
            this.current = start;

            this.array = array;
        }
        
        public static RefRange<T> Null()
        {
            return new RefRange<T>();
        }
    }
}