

namespace ReGizmo.Drawing
{
    internal static class ReGizmoResolver<TDrawer>
        where TDrawer : class, IReGizmoDrawer
    {
        static TDrawer _drawerSorted;
        static TDrawer _drawerOverlay;

        public static TDrawer DrawerSorted => _drawerSorted;
        public static TDrawer DrawerOverlay => _drawerOverlay;

        public static void Init(System.Func<TDrawer> producer)
        {
            _drawerSorted = producer.Invoke();
            _drawerOverlay = producer.Invoke();
            _drawerOverlay.SetDepthMode(DepthMode.Overlay);
        }

        public static bool TryGet(out TDrawer drawer, DepthMode depthMode = DepthMode.Sorted)
        {
            switch (depthMode)
            {
                case DepthMode.Sorted:
                    drawer = _drawerSorted;
                    break;
                case DepthMode.Overlay:
                    drawer = _drawerOverlay;
                    break;
                default:
                    drawer = null;
                    break;
            }

            if (drawer == null)
            {
                return false;
            }

            return true;
        }
    }
}