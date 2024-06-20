using Context;
using Game.Enemy.ActionsExecutor;
using UnityEngine;

namespace Game.Enemy.Context
{
    public interface IEnemyContextBase : IContext
    {
        IDefaultControllableEnemy DefaultControllableEnemy { get; }
        Transform Transform { get; }
    }
}