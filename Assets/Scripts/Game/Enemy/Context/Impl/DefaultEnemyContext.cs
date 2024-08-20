using Game.Enemy.ActionsExecutor;
using UnityEngine;

namespace Game.Enemy.Context.Impl
{
    public class DefaultEnemyContext : IEnemyContextBase
    {
        public IDefaultControllableEnemy DefaultControllableEnemy { get; }
        public Transform Transform { get; }

        public DefaultEnemyContext(
            IDefaultControllableEnemy defaultControllableEnemy,
            Transform transform
        )
        {
            DefaultControllableEnemy = defaultControllableEnemy;
            Transform = transform;
        }
    }
}