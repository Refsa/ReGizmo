

namespace ReGizmo.Utils
{
    public ref struct RefRange<T>
        where T : unmanaged
    {
        T[] array;
        int start;
        int end;
        int current;

        public RefRange(T[] array, int start, int end)
        {
            this.start = start;
            this.end = end;
            this.current = start;

            this.array = array;
        }

        public bool IsValid() => array != null;

        public bool HasNext()
        {
            return current < end;
        }

        public ref T Next()
        {
            if (!HasNext())
            {
                throw new System.IndexOutOfRangeException($"Trying to retreive element outside of range | start: {start} - end: {end} - current: {current}");
            }

            return ref array[current++];
        }

        public static RefRange<T> Null()
        {
            return new RefRange<T>();
        }
    }
}