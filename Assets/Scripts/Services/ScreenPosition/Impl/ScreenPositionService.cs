using UnityEngine;

namespace Services.ScreenPosition.Impl
{
    public class ScreenPositionService : IScreenPositionService
    {
        public Vector2 ConvertScreenPositionToWorld(Vector2 screenPosition)
        {
            return Camera.main.ScreenToWorldPoint(screenPosition);
        }
    }
}