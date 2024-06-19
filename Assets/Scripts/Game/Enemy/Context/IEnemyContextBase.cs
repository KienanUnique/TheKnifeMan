using Context;
using Game.Enemy.ActionsExecutor;
using Game.Utils;
using UnityEngine;

namespace Game.Enemy.Context
{
    public interface IEnemyContextBase : IContext
    {
        EEnemyType Type { get; }
        IDefaultActionsExecutor DefaultActionsExecutor { get; }
        Transform Transform { get; }
    }
}