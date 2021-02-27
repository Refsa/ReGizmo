
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ReGizmo.Drawing
{
    internal class RePool<T>
    {
        bool initialized = false;
        IRePoolFactory<T> _factory;
        RePoolContainer<T> _container;

        public RePool<T> Init<TFactory>(TFactory factory, int capacity, bool resetOnFree = false) where TFactory : IRePoolFactory<T>
        {
            _factory = factory;
            _container = new RePoolContainer<T>(_factory.Generate, _factory.Reset, capacity, resetOnFree);

            initialized = true;

            return this;
        }

        public T Get()
        {
            if (!initialized)
            {
                throw new InvalidOperationException($"RePool<{typeof(T)}> is not initialized");
            }

            return _container.Get();
        }

        public void Free(T target)
        {
            if (!initialized)
            {
                throw new InvalidOperationException($"RePool<{typeof(T)}> is not initialized");
            }

            _container.Free(target);
        }
    }

    public interface IRePoolFactory<T>
    {
        T Generate();
        void Reset(T target);
    }

    public class RePoolContainer<T>
    {
        int capacity;
        Queue<T> pool;
        bool resetOnFree;

        Func<T> factory;
        Action<T> reset;

        public RePoolContainer(Func<T> factory, Action<T> reset, int capacity, bool resetOnFree)
        {
            this.factory = factory;
            this.reset = reset;
            this.resetOnFree = resetOnFree;

            pool = new Queue<T>();

            Expand(capacity);
        }

        public T Get()
        {
            if (pool.Count == 0)
            {
                Expand();
            }

            var item = pool.Dequeue();

            if (item == null) return Get();
            return item;
        }

        public void Free(T target)
        {
            if (target == null)
            {
                throw new ArgumentNullException($"Trying to release a null target of {typeof(T)}");
            }

            if (resetOnFree)
                reset.Invoke(target);

            pool.Enqueue(target);
        }

        void Expand(int count = 128)
        {
            Console.WriteLine($"Expanding pool from {capacity} to {capacity + count}");

            for (int i = 0; i < count; i++)
            {
                var instance = factory.Invoke();
                if (instance != null)
                {
                    pool.Enqueue(instance);
                }
            }

            capacity += count;
        }
    }
}