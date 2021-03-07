
using System;
using ReGizmo.Drawing;

namespace ReGizmo.Utility
{
    public class Maybe<T>
    {
        public T Value;

        T defaultValue;

        public Maybe(T defaultValue)
        {
            this.defaultValue = defaultValue;
            Value = this.defaultValue;
        }

        public void Set(T value)
        {
            this.Value = value;
        }

        public void Reset()
        {
            this.Value = defaultValue;
        }

        public MaybeScope<T> Scope(T value)
        {
            return new MaybeScope<T>(this, value);
        }

        public static implicit operator T(Maybe<T> from)
        {
            return from.Value;
        }

        public static implicit operator Maybe<T>(T from)
        {
            return new Maybe<T>(from);
        }
    }

    public struct MaybeScope<T> : IDisposable
    {
        Maybe<T> scoped;

        public MaybeScope(Maybe<T> toScope, T value)
        {
            scoped = toScope;
            scoped.Set(value);
        }

        public void Dispose()
        {
            scoped.Reset();
        }
    }
}