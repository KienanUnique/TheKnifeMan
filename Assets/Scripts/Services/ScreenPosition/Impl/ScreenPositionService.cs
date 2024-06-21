using UnityEngine;

namespace Services.ScreenPosition.Impl
{
    public class ScreenPositionService : IScreenPositionService
    {
        public Vector2 ConvertScreenPositionToWorld(Vector2 screenPosition)
        {
            var camera = Camera.main;
            var newScreenPosition = new Vector3(screenPosition.x, screenPosition.y, -camera.transform.position.z);
            return camera.ScreenToWorldPoint(newScreenPosition);
        }
    }
}