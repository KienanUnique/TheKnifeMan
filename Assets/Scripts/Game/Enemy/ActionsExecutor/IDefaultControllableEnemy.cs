using UnityEngine;

namespace Game.Enemy.ActionsExecutor
{
    public interface IDefaultControllableEnemy
    {
        bool SetDestination(Vector3 position);
        bool IsInAction { get; }

        void EnableMoving();
        void DisableMoving();
    }
}