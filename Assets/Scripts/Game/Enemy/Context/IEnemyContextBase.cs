using Game.Enemy.ActionsExecutor;
using Game.Utils;
using TheKiwiCoder.Context;

namespace Game.Enemy.Context
{
    public interface IEnemyContextBase : IContext
    {
        EEnemyType Type { get; }
        IDefaultActionsExecutor DefaultActionsExecutor { get; }
    }
}