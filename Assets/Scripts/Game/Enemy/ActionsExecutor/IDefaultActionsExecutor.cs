using UnityEngine;

namespace Game.Enemy.ActionsExecutor
{
    public interface IDefaultActionsExecutor
    {
        bool SetDestination(Vector3 position);
    }
}