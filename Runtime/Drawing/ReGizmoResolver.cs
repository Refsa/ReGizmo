

namespace ReGizmo.Drawing
{
    internal static class ReGizmoResolver<TDrawer>
        where TDrawer : IReGizmoDrawer
    {
        static TDrawer _drawer;

        public static TDrawer Init(TDrawer drawer)
        {
            _drawer = drawer;

            return drawer;
        }

        public static bool TryGet(out TDrawer drawer)
        {
            drawer = _drawer;
            if (drawer == null)
            {
                return false;
            }

            return true;
        }
    }
}