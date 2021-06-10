using System.Collections;
using System.Collections.Generic;
using ReGizmo.Drawing;
using UnityEngine;

namespace ReGizmo.Samples
{
    public class LTreeSample : MonoBehaviour
    {
        enum LTreeType
        {
            Fern,
            Weed,
        }

        [SerializeField] bool rebuild = false;
        [SerializeField] LTreeType type;
        [SerializeField] int generations = 4;
        ILTree _ltree;

        void OnDrawGizmos()
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();

            if (rebuild)
            {
                switch (type)
                {
                    case LTreeType.Fern:
                        _ltree = new FernLSys.FernLTree();
                        break;
                    case LTreeType.Weed:
                        _ltree = new WeedLSys.WeedLTree();
                        break;
                }
                _ltree.Generate(generations);
                _ltree.Build();
                rebuild = false;
            }

            if (_ltree == null) return;
            _ltree.Draw();

            sw.Stop();
            Debug.Log($"elapsed {sw.ElapsedTicks / 10_000f} ms");
        }
    }
}