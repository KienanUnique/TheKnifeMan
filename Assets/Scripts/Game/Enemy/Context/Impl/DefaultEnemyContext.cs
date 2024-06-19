using Game.Enemy.ActionsExecutor;
using Game.Utils;
using UnityEngine;

namespace Game.Enemy.Context.Impl
{
    public class DefaultEnemyContext : IEnemyContextBase
    {
        public EEnemyType Type { get; }
        public IDefaultActionsExecutor DefaultActionsExecutor { get; }
        public Transform Transform { get; }

        public DefaultEnemyContext(
            EEnemyType type,
            IDefaultActionsExecutor defaultActionsExecutor,
            Transform transform
        )
        {
            Type = type;
            DefaultActionsExecutor = defaultActionsExecutor;
            Transform = transform;
        }
    }
}