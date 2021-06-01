
using System.Collections.Generic;
using UnityEngine;

namespace ReGizmo
{
    internal class CameraFrustum
    {
        Camera camera;
        UnityEngine.Plane[] tempPlanes = new UnityEngine.Plane[6];
        Vector4[] frustumPlanes;

        public CameraFrustum(Camera camera)
        {
            this.camera = camera;
            frustumPlanes = new Vector4[6];
        }

        public Vector4[] UpdateCameraFrustum()
        {
            GeometryUtility.CalculateFrustumPlanes(camera, tempPlanes);

            frustumPlanes[0] = new Vector4(tempPlanes[0].normal.x, tempPlanes[0].normal.y, tempPlanes[0].normal.z, tempPlanes[0].distance);
            frustumPlanes[1] = new Vector4(tempPlanes[1].normal.x, tempPlanes[1].normal.y, tempPlanes[1].normal.z, tempPlanes[1].distance);
            frustumPlanes[2] = new Vector4(tempPlanes[2].normal.x, tempPlanes[2].normal.y, tempPlanes[2].normal.z, tempPlanes[2].distance);
            frustumPlanes[3] = new Vector4(tempPlanes[3].normal.x, tempPlanes[3].normal.y, tempPlanes[3].normal.z, tempPlanes[3].distance);
            frustumPlanes[4] = new Vector4(tempPlanes[4].normal.x, tempPlanes[4].normal.y, tempPlanes[4].normal.z, tempPlanes[4].distance);
            frustumPlanes[5] = new Vector4(tempPlanes[5].normal.x, tempPlanes[5].normal.y, tempPlanes[5].normal.z, tempPlanes[5].distance);

            return frustumPlanes;
        }
    }
}