using Game.Enemy.ActionsExecutor;
using Game.Enemy.Context;

namespace Game.Enemy.Nodes
{
    public abstract class AAiActionNode<TContext, TActionExecutor> : ActionNodeWithContext<IEnemyContextBase>
        where TContext : IEnemyContextBase
        where TActionExecutor : IDefaultActionsExecutor
    {
        protected sealed override void HandleInitialize()
        {
            base.HandleInitialize();
            
        }
        
        protected abstract HandleInitialize
    }
}