using System;
using Game.Enemy.ActionsExecutor;
using Game.Enemy.Context;

namespace Game.Enemy.Nodes.Actions
{
    [Serializable]
    public class AttackMelee : ActionNodeWithContext<IEnemyContextBase>
    {
        private IMeleeAttackerActionsExecutor _meleeAttackerActionsExecutor;

        protected override void HandleInitialize()
        {
            base.HandleInitialize();
            if (ConcreteContext.DefaultActionsExecutor is IMeleeAttackerActionsExecutor meleeAttackerActionsExecutor)
                _meleeAttackerActionsExecutor = meleeAttackerActionsExecutor;
            else
                throw new InvalidCastException();
        }

        protected override ENodeState OnUpdate()
        {
            _meleeAttackerActionsExecutor.AttackMelee();
            return ENodeState.Success;
        }
    }
}