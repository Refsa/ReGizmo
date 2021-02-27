using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ReGizmo.Drawing
{
    public struct PolyLine : System.IDisposable, IEnumerable
    {
        const int StartFlag = 1 << 1;
        const int EndFlag = 1 << 2;

        public List<PolyLineData> Points;
        public int Looping;
        /// <summary>
        /// Automatically Dispose after PolyLine has been drawn
        /// </summary>
        public bool AutoDispose;
        /// <summary>
        /// Draw when Dispose is called
        /// </summary>
        public bool AutoDraw;

        uint id;
        bool startSet;

        public int Count => Points.Count;
        public uint ID => id;
        public bool Initialized{get; private set;}

        public PolyLineData this[int index]
        {
            get => Points[index];
            set => Points[index] = value;
        }

        public PolyLine(bool looping = false)
        {
            Looping = looping ? 1 : 0;
            Points = PolyLinePool.Get();
            Initialized = true;
            id = GenerateID();
            startSet = false;
            AutoDispose = false;
            AutoDraw = false;
        }

        /// <summary>
        /// Start a PolyLine scope, to be used with "using" keyword
        /// </summary>
        /// <returns>PolyLine setup for "using" keyword</returns>
        public static PolyLine Scope()
        {
            var pl = new PolyLine();
            pl.Initialize();
            pl.AutoDraw = true;
            return pl;
        }

        public void Dispose()
        {
            if (Initialized)
            {
                // if (AutoDraw)
                    // ReGizmo.DrawPolyLine(ref this, false);

                PolyLinePool.Release(Points);
            }

            Points = null;
            Initialized = false;
        }

        public IEnumerator GetEnumerator()
        {
            return Points.GetEnumerator();
        }

        public void Add(PolyLineData ld)
        {
            Initialize();

            ld.ID.x = id;
            Points.Add(ld);
        }

        bool Initialize()
        {
            if (Initialized) return false;

            if (Points == null)
                Points = PolyLinePool.Get();

            id = GenerateID();
            startSet = false;
            AutoDispose = true;

            Initialized = true;
            return true;
        }

        internal void SetLastID()
        {
            var lastPoint = Points[Points.Count - 1];

            lastPoint.ID.y = EndFlag;
            lastPoint.ID.z = (Looping * (Points.Count - 2));

            Points[Points.Count - 1] = lastPoint;
        }

        internal void SetStartID()
        {
            var start = Points[0];
            start.ID.y = StartFlag;
            start.ID.z = (Looping * (Points.Count - 2));

            Points[0] = start;
        }

        // TODO: This should generate a hash with lower chance of collision
        static uint GenerateID()
        {
            uint ID = (uint)Random.Range(1000, 999999999);
            while (ID < 999999999u) ID += (uint)Random.Range(1000, 999999999) % ID;

            return ID;
        }
    }
}