using Context;
using Game.Enemy.ActionsExecutor;
using Game.Utils;

namespace Game.Enemy.Context
{
    public interface IEnemyContextBase : IContext
    {
        EEnemyType Type { get; }
        IDefaultActionsExecutor DefaultActionsExecutor { get; }
    }
}