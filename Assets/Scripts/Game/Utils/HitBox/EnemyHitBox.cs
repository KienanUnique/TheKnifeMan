using Alchemy.Inspector;
using Game.Enemy;
using Game.Interfaces;
using UnityEngine;

namespace Game.Utils.HitBox
{
    public class EnemyHitBox : MonoBehaviour
    {
        [ValidateInput(nameof(IsControllerValid))] 
        [SerializeField]
        private GameObject controller;

        public static bool IsControllerValid(GameObject gameObject)
        {
            return gameObject != null && gameObject.TryGetComponent(out IPoolEnemy _);
        }

        public bool TryGet<T>(out T component)
        {
            if (controller.TryGetComponent(out T result))
            {
                component = result;
                return true;
            }

            component = default;
            return false;
        }

        [Button]
        public void AutoFill()
        {
            var parent = transform.parent.gameObject;
            
            if (IsControllerValid(parent))
                controller = parent;
        }
    }
}