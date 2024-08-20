using UnityEngine;

namespace Services.ScreenPosition
{
    public interface IScreenPositionService
    {
        Vector2 ConvertScreenPositionToWorld(Vector2 screenPosition);
    }
}