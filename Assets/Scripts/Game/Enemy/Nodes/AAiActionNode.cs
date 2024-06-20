using System;
using Game.Enemy.ActionsExecutor;
using Game.Enemy.Context;

namespace Game.Enemy.Nodes
{
    public abstract class AAiActionNode<TContext, TEnemy> : ActionNodeWithContext<TContext>
        where TContext : class, IEnemyContextBase
        where TEnemy : IDefaultControllableEnemy
    {
        protected TEnemy Enemy { get; private set; }

        protected sealed override void HandleInitialize()
        {
            base.HandleInitialize();

            var enemy = ConcreteContext.DefaultControllableEnemy;
            if (enemy is TEnemy concreteEnemy)
                Enemy = concreteEnemy;
            else
                throw new InvalidCastException(
                    $"Can't cast enemy to {typeof(TEnemy)}");

            Initialize();
        }

        protected virtual void Initialize()
        {
        }
    }

    public abstract class AAiActionNode<TEnemy> : ActionNodeWithContext<IEnemyContextBase>
        where TEnemy : IDefaultControllableEnemy
    {
        protected TEnemy Enemy { get; private set; }

        protected sealed override void HandleInitialize()
        {
            base.HandleInitialize();

            var enemy = ConcreteContext.DefaultControllableEnemy;
            if (enemy is TEnemy concreteEnemy)
                Enemy = concreteEnemy;
            else
                throw new InvalidCastException(
                    $"Can't cast enemy to {typeof(TEnemy)}");

            Initialize();
        }

        protected virtual void Initialize()
        {
        }
    }

    public abstract class AAiActionNode : ActionNodeWithContext<IEnemyContextBase>
    {
        protected IDefaultControllableEnemy Enemy { get; private set; }

        protected sealed override void HandleInitialize()
        {
            base.HandleInitialize();

            Enemy = ConcreteContext.DefaultControllableEnemy;

            Initialize();
        }

        protected virtual void Initialize()
        {
        }
    }
}