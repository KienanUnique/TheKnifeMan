using Game.Enemy.ActionsExecutor;
using Game.Utils;

namespace Game.Enemy.Context.Impl
{
    public class DefaultEnemyContext : IEnemyContextBase
    {
        public EEnemyType Type { get; }
        public IDefaultActionsExecutor DefaultActionsExecutor { get; }
        

        public DefaultEnemyContext(EEnemyType type, IDefaultActionsExecutor defaultActionsExecutor)
        {
            Type = type;
            DefaultActionsExecutor = defaultActionsExecutor;
        }
    }
}