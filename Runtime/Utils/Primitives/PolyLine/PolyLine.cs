using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ReGizmo.Drawing
{
    public struct PolyLine : System.IDisposable
    {
        const int StartFlag = 1 << 1;
        const int EndFlag = 1 << 2;

        public List<PolyLineData> Points;
        public bool Looping;
        /// <summary>
        /// Automatically Dispose after PolyLine has been drawn
        /// </summary>
        public bool AutoDispose;
        /// <summary>
        /// Draw when Dispose is called
        /// </summary>
        public bool AutoDraw;

        int id;

        public bool Initialized { get; private set; }
        public int ID => id;

        public PolyLineData this[int index]
        {
            get => Points[index];
            set => Points[index] = value;
        }

        public PolyLine(bool looping = false)
        {
            Looping = looping;
            Points = PolyLinePool.Get();
            Initialized = true;
            id = GenerateID();
            AutoDispose = true;
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
            this.AutoDispose = false;
            if (!Initialized) return;

            if (Initialized && AutoDraw)
            {
                this.Draw();
            }

            if (Points != null) PolyLinePool.Release(Points);

            Points = null;
            Initialized = false;
        }

        public void Add(PolyLineData ld)
        {
            Initialize();

            ld.ID = id;
            Points.Add(ld);
        }

        bool Initialize()
        {
            if (Initialized) return false;

            if (Points == null)
                Points = PolyLinePool.Get();

            id = GenerateID();
            AutoDispose = true;

            Initialized = true;
            return true;
        }

        // TODO: This should generate a hash with lower chance of collision
        static int GenerateID()
        {
            int ID = Random.Range(-999999999, 999999999);
            return ID;
        }
    }
}