using UnityEngine;

namespace Game.Utils.HitBox
{
    public static class ComponentHitBoxExtensions
    {
        public static bool TryGetEnemyComponent<T>(this Component component, out T result)
        {
            if (component.TryGetComponent(out EnemyHitBox hitBox) && hitBox.TryGet<T>(out var foundedResult))
            {
                result = foundedResult;
                return true;
            }

            result = default;
            return false;
        }
    }
}